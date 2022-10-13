using AppModels.AppModels.Order;
using AppModels.AppModels.Portfolio;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using EntityFrameworkCore.UnitOfWork.Factories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace AppServices.Services
{
    public class PortfolioAppService : IPortfolioAppService
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IMapper _mapper;
        private readonly ICustomerBankInfoAppService _customerBankInfoAppService;
        private readonly IProductAppService _productAppService;
        private readonly IOrderAppService _orderAppService;
        private readonly IPortfolioProductService _portfolioProductService;

        public PortfolioAppService(
            IPortfolioService portfolio,
            IMapper mapper,
            ICustomerBankInfoAppService customerBankInfoAppServices,
            IProductAppService productAppServices,
            IOrderAppService orderAppServices,
            IPortfolioProductService portfolioProductServices)
        {
            _portfolioService = portfolio ?? throw new ArgumentNullException(nameof(portfolio));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _customerBankInfoAppService = customerBankInfoAppServices ?? throw new ArgumentNullException(nameof(customerBankInfoAppServices));
            _productAppService = productAppServices ?? throw new ArgumentNullException(nameof(productAppServices));
            _orderAppService = orderAppServices ?? throw new ArgumentNullException(nameof(orderAppServices));
            _portfolioProductService = portfolioProductServices ?? throw new ArgumentNullException(nameof(portfolioProductServices));
        }

        public async Task<long> CreateAsync(CreatePortfolio model)
        {
            Portfolio portfolio = _mapper.Map<Portfolio>(model);
            return await _portfolioService.CreateAsync(portfolio).ConfigureAwait(false);
        }

        public IEnumerable<PortfolioResponse> GetAll()
        {
            var result = _portfolioService.GetAll();
            return _mapper.Map<IEnumerable<PortfolioResponse>>(result);
        }

        public async Task<PortfolioResponse> GetByIdAsync(long id)
        {
            var result = await _portfolioService.GetByIdAsync(id).ConfigureAwait(false);
            return _mapper.Map<PortfolioResponse>(result);
        }

        public decimal GetAccountBalance(long portfolioId)
        {
            return _portfolioService.GetAccountBalance(portfolioId);
        }

        public void Deposit(decimal amount, long customerId, long portfolioId)
        {
            if (_customerBankInfoAppService.GetBalance(customerId) < amount)
            {
                throw new ArgumentException("Não há saldo suficiente na conta corrente para realizar este depósito");
            }

            using var transactionScope = TransactionScopeFactory.CreateTransactionScope();
            _customerBankInfoAppService.Withdraw(customerId, amount);
            _portfolioService.Deposit(amount, portfolioId);
            transactionScope.Complete();
        }

        public void Withdraw(decimal amount, long customerId, long portfolioId)
        {
            if (_portfolioService.GetAccountBalance(portfolioId) < amount)
            {
                throw new ArgumentException("Não há saldo suficiente na carteira para realizar o saque requerido");
            }

            using var transactionScope = TransactionScopeFactory.CreateTransactionScope();
            _portfolioService.Withdraw(amount, portfolioId);
            _customerBankInfoAppService.Deposit(customerId, amount);
            transactionScope.Complete();
        }

        public async Task InvestAsync(int quotes, DateTime liquidateAt, long productId, long portfolioId)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var product = await _productAppService.GetByIdAsync(productId);
            var order = new CreateOrder(quotes, product.UnitPrice,
                                        liquidateAt, AppModels.EnumModels.OrderEnum.Buy, productId, portfolioId);
            var orderId = await _orderAppService.CreateAsync(order).ConfigureAwait(false);

            if (GetAccountBalance(portfolioId) < order.NetValue)
            {
                throw new ArgumentException("Não há saldo suficiente na carteira para realizar este investimento");
            }

            if (DateTime.Now.Date >= liquidateAt.Date)
            {
                var orderResult = await _orderAppService.GetByIdAsync(orderId);
                await ExecuteBuyOrderAsync(orderResult);
            }
            transactionScope.Complete();
        }

        public async Task WithdrawProduct(int quotes, DateTime liquidateAt, long productId, long portfolioId)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var product = await _productAppService.GetByIdAsync(productId).ConfigureAwait(false);
            var portfolio = await _portfolioService.GetByIdAsync(portfolioId).ConfigureAwait(false);
            int availableQuotes = _orderAppService.GetAvailableQuotes(portfolioId, productId);

            if (quotes > availableQuotes)
            {
                throw new ArgumentException("A quantidade de cotas informada é maior do que as cotas existentes na carteira");
            }

            var createOrder = new CreateOrder(quotes, product.UnitPrice,
                                        liquidateAt, AppModels.EnumModels.OrderEnum.Sell, productId, portfolioId);
            var orderId = await _orderAppService.CreateAsync(createOrder).ConfigureAwait(false);

            if (DateTime.Now.Date >= liquidateAt.Date)
            {
                var orderResult = await _orderAppService.GetByIdAsync(orderId);
                await ExecuteSellOrderAsync(orderResult);
            }
            transactionScope.Complete();
        }

        public async Task ExecuteTodaysOrdersAsync()
        {
            var orders = _orderAppService.GetOrdersToExecute();

            foreach (var order in orders)
            {
                if (order.Direction == AppModels.EnumModels.OrderEnum.Buy)
                {
                    await ExecuteBuyOrderAsync(order);
                }
                else
                {
                    await ExecuteSellOrderAsync(order);
                }
            }
        }

        public async Task ExecuteBuyOrderAsync(OrderResponse order)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var portfolio = await _portfolioService.GetByIdAsync(order.PortfolioId);
            var productResult = await _productAppService.GetByIdAsync(order.ProductId);
            var product = _mapper.Map<Product>(productResult);

            _portfolioService.ExecuteBuyOrder(order.NetValue, order.PortfolioId);


            if (!_portfolioProductService.RelationExists(order.PortfolioId, order.ProductId))
            {
                await _portfolioProductService.CreateRelationAsync(portfolio, product);
            }
            transactionScope.Complete();
        }

        public async Task ExecuteSellOrderAsync(OrderResponse order)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var portfolio = await _portfolioService.GetByIdAsync(order.PortfolioId);
            var productResult = await _productAppService.GetByIdAsync(order.ProductId);
            var product = _mapper.Map<Product>(productResult);

            _portfolioService.ExecuteSellOrder(order.NetValue, order.PortfolioId);

            int availableQuotes = _orderAppService.GetAvailableQuotes(order.PortfolioId, order.ProductId);

            if (availableQuotes == 0)
            {
                await _portfolioProductService.DeleteRelationAsync(portfolio, product);

            }
            transactionScope.Complete();
        }

        public void Delete(long portfolioId)
        {
            _portfolioService.Delete(portfolioId);
        }
    }
}
