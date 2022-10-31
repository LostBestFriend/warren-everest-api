using AppModels.AppModels.Orders;
using AppModels.AppModels.Portfolios;
using AppModels.EnumModels;
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
            _portfolioService = portfolio ??
                throw new ArgumentNullException(nameof(portfolio));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _customerBankInfoAppService = customerBankInfoAppServices ??
                throw new ArgumentNullException(nameof(customerBankInfoAppServices));
            _productAppService = productAppServices ??
                throw new ArgumentNullException(nameof(productAppServices));
            _orderAppService = orderAppServices ??
                throw new ArgumentNullException(nameof(orderAppServices));
            _portfolioProductService = portfolioProductServices ??
                throw new ArgumentNullException(nameof(portfolioProductServices));
        }

        public async Task<long> CreateAsync(CreatePortfolio model)
        {
            var portfolio = _mapper.Map<Portfolio>(model);
            return await _portfolioService.CreateAsync(portfolio).ConfigureAwait(false);
        }

        public async Task<IEnumerable<PortfolioResponse>> GetAllAsync()
        {
            var result = await _portfolioService.GetAllAsync().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<PortfolioResponse>>(result);
        }

        public async Task<PortfolioResponse> GetByIdAsync(long id)
        {
            var result = await _portfolioService.GetByIdAsync(id).ConfigureAwait(false);
            return _mapper.Map<PortfolioResponse>(result);
        }

        public async Task<decimal> GetAccountBalanceAsync(long portfolioId)
        {
            return await _portfolioService.GetAccountBalanceAsync(portfolioId).ConfigureAwait(false);
        }

        public async Task DepositAsync(decimal amount, long customerId, long portfolioId)
        {
            if (await _customerBankInfoAppService.GetBalanceAsync(customerId).ConfigureAwait(false) < amount)
                throw new ArgumentException("Não há saldo suficiente na conta corrente para realizar este depósito");

            using var transactionScope = TransactionScopeFactory.CreateTransactionScope();
            await _customerBankInfoAppService.WithdrawAsync(customerId, amount).ConfigureAwait(false);
            await _portfolioService.DepositAsync(amount, portfolioId).ConfigureAwait(false);
            transactionScope.Complete();
        }

        public async Task WithdrawAsync(decimal amount, long customerId, long portfolioId)
        {
            if (await _portfolioService.GetAccountBalanceAsync(portfolioId).ConfigureAwait(false) < amount)
                throw new ArgumentException("Não há saldo suficiente na carteira para realizar o saque requerido");

            using var transactionScope = TransactionScopeFactory.CreateTransactionScope();
            await _portfolioService.WithdrawAsync(amount, portfolioId).ConfigureAwait(false);
            await _customerBankInfoAppService.DepositAsync(customerId, amount).ConfigureAwait(false);
            transactionScope.Complete();
        }

        public async Task InvestAsync(int quotes, DateTime liquidateAt, long productId, long portfolioId)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var product = await _productAppService.GetByIdAsync(productId).ConfigureAwait(false);
            var order = new CreateOrder(quotes, product.UnitPrice,
                                        liquidateAt, OrderDirection.Buy, productId, portfolioId);
            var orderId = await _orderAppService.CreateAsync(order).ConfigureAwait(false);

            if (await GetAccountBalanceAsync(portfolioId).ConfigureAwait(false) < order.NetValue)
                throw new ArgumentException("Não há saldo suficiente na carteira para realizar este investimento");

            if (DateTime.Now.Date >= liquidateAt.Date)
            {
                var orderResult = await _orderAppService.GetByIdAsync(orderId).ConfigureAwait(false);
                await ExecuteBuyOrderAsync(orderResult).ConfigureAwait(false);
            }
            transactionScope.Complete();
        }

        public async Task WithdrawProductAsync(int quotes, DateTime liquidateAt, long productId, long portfolioId)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var product = await _productAppService.GetByIdAsync(productId).ConfigureAwait(false);
            var portfolio = await _portfolioService.GetByIdAsync(portfolioId).ConfigureAwait(false);
            int availableQuotes = await _orderAppService.GetQuotesAvaliableAsync(portfolioId, productId).ConfigureAwait(false);

            if (quotes > availableQuotes)
                throw new ArgumentException("A quantidade de cotas informada é maior do que as cotas existentes na carteira");

            var createOrder = new CreateOrder(quotes, product.UnitPrice,
                                        liquidateAt, OrderDirection.Sell, productId, portfolioId);
            var orderId = await _orderAppService.CreateAsync(createOrder).ConfigureAwait(false);

            if (DateTime.Now.Date >= liquidateAt.Date)
            {
                var orderResult = await _orderAppService.GetByIdAsync(orderId).ConfigureAwait(false);
                await ExecuteSellOrderAsync(orderResult).ConfigureAwait(false);
            }
            transactionScope.Complete();
        }

        public async Task ExecuteNowOrdersAsync()
        {
            var orders = await _orderAppService.GetExecutableOrdersAsync().ConfigureAwait(false);

            foreach (var order in orders)
            {
                if (order.Direction == OrderDirection.Buy)
                {
                    await ExecuteBuyOrderAsync(order).ConfigureAwait(false);
                }
                else
                {
                    await ExecuteSellOrderAsync(order).ConfigureAwait(false);
                }
            }
        }

        public async Task ExecuteBuyOrderAsync(OrderResponse order)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var portfolio = await _portfolioService.GetByIdAsync(order.PortfolioId).ConfigureAwait(false);
            var productResult = await _productAppService.GetByIdAsync(order.ProductId).ConfigureAwait(false);
            var product = _mapper.Map<Product>(productResult);

            await _portfolioService.ExecuteBuyOrderAsync(order.NetValue, order.PortfolioId).ConfigureAwait(false);

            if (!await _portfolioProductService.RelationAlreadyExistsAsync(order.PortfolioId, order.ProductId).ConfigureAwait(false))
            {
                await _portfolioProductService.InitRelationAsync(portfolio, product).ConfigureAwait(false);
            }
            transactionScope.Complete();
        }

        public async Task ExecuteSellOrderAsync(OrderResponse order)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var portfolio = await _portfolioService.GetByIdAsync(order.PortfolioId).ConfigureAwait(false);
            var productResult = await _productAppService.GetByIdAsync(order.ProductId).ConfigureAwait(false);
            var product = _mapper.Map<Product>(productResult);

            await _portfolioService.ExecuteSellOrderAsync(order.NetValue, order.PortfolioId).ConfigureAwait(false);

            int availableQuotes = await _orderAppService.GetQuotesAvaliableAsync(order.PortfolioId, order.ProductId).ConfigureAwait(false);

            if (availableQuotes == 0)
            {
                await _portfolioProductService.DisposeRelationAsync(portfolio, product).ConfigureAwait(false);

            }
            transactionScope.Complete();
        }

        public async Task DeleteAsync(long portfolioId)
        {
            await _portfolioService.DeleteAsync(portfolioId).ConfigureAwait(false);
        }
    }
}
