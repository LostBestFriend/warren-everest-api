using AppModels.AppModels.Orders;
using AppModels.EnumModels;
using AppServices.Interfaces;
using AppServices.Profiles;
using AppServices.Services;
using AppServices.Tests.Fixtures.Order;
using AppServices.Tests.Fixtures.Portfolio;
using AppServices.Tests.Fixtures.Product;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using DomainServices.Tests.Fixtures;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AppServices.Tests.Services
{
    public class PortfolioAppServiceTest
    {
        private readonly Mock<IPortfolioService> _portfolioServiceMock;
        private readonly Mock<ICustomerBankInfoAppService> _customerBankInfoAppServiceMock;
        private readonly Mock<IProductAppService> _productAppServiceMock;
        private readonly Mock<IOrderAppService> _orderAppServiceMock;
        private readonly Mock<IPortfolioProductService> _portfolioProductServiceMock;
        private readonly PortfolioAppService _portfolioAppService;
        private readonly IMapper _mapper;

        public PortfolioAppServiceTest()
        {

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PortfolioProfile>();
                cfg.AddProfile<ProductProfile>();
            }).CreateMapper();
            _portfolioServiceMock = new();
            _customerBankInfoAppServiceMock = new();
            _productAppServiceMock = new();
            _orderAppServiceMock = new();
            _portfolioProductServiceMock = new();
            _portfolioAppService = new PortfolioAppService(portfolio: _portfolioServiceMock.Object, mapper: _mapper, customerBankInfoAppServices: _customerBankInfoAppServiceMock.Object, productAppServices: _productAppServiceMock.Object, orderAppServices: _orderAppServiceMock.Object, portfolioProductServices: _portfolioProductServiceMock.Object);
        }

        [Fact]
        public async void Should_CreateAsync_Sucessfully()
        {
            var createPortfolio = CreatePortfolioFixture.GenerateCreatePortfolioFixture();
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            long id = 1;

            _portfolioServiceMock.Setup(p => p.CreateAsync(It.IsAny<Portfolio>())).ReturnsAsync(id);

            var result = await _portfolioAppService.CreateAsync(createPortfolio);

            result.Should().BeGreaterThanOrEqualTo(0);

            _portfolioServiceMock.Verify(p => p.CreateAsync(It.IsAny<Portfolio>()), Times.Once);
        }

        [Fact]
        public async Task Should_GetAllAsync_Sucessfully()
        {
            var portfolios = PortfolioFixture.GeneratePortfolioFixture(3);
            var portfolioResponses = PortfolioResponseFixture.GeneratePortfolioResponseFixture(3);

            _portfolioServiceMock.Setup(p => p.GetAllAsync()).ReturnsAsync(portfolios);

            var result = await _portfolioAppService.GetAllAsync();

            result.Should().NotBeNull();

            _portfolioServiceMock.Verify(p => p.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async void Should_GetByIdAsync_Sucessfully()
        {
            long id = 1;
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();

            _portfolioServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(portfolio);

            var result = await _portfolioAppService.GetByIdAsync(id);

            result.Should().NotBeNull();

            _portfolioServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async void Should_GetAccountBalance_Sucessfully()
        {
            long portfolioId = 1;
            decimal balance = 1000;

            _portfolioServiceMock.Setup(p => p.GetAccountBalanceAsync(It.IsAny<long>())).ReturnsAsync(balance);

            var result = await _portfolioAppService.GetAccountBalanceAsync(portfolioId);

            result.Should().BeGreaterThanOrEqualTo(0);

            _portfolioServiceMock.Verify(p => p.GetAccountBalanceAsync(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async Task Should_Deposit_Sucessfully()
        {
            long portfolioId = 1;
            long customerId = 1;
            decimal amount = 100;
            decimal balance = 1000;

            _customerBankInfoAppServiceMock.Setup(p => p.GetBalanceAsync(It.IsAny<long>())).ReturnsAsync(balance);
            _customerBankInfoAppServiceMock.Setup(p => p.WithdrawAsync(It.IsAny<long>(), It.IsAny<decimal>()));
            _portfolioServiceMock.Setup(p => p.DepositAsync(It.IsAny<decimal>(), It.IsAny<long>()));

            await _portfolioAppService.DepositAsync(amount, customerId, portfolioId);

            _customerBankInfoAppServiceMock.Verify(p => p.GetBalanceAsync(It.IsAny<long>()), Times.Once);
            _customerBankInfoAppServiceMock.Verify(p => p.WithdrawAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
            _portfolioServiceMock.Verify(p => p.DepositAsync(It.IsAny<decimal>(), It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Deposit_Sucessfully()
        {
            long portfolioId = 1;
            long customerId = 1;
            decimal amount = 100;
            decimal balance = 10;

            _customerBankInfoAppServiceMock.Setup(p => p.GetBalanceAsync(It.IsAny<long>())).ReturnsAsync(balance);
            _customerBankInfoAppServiceMock.Setup(p => p.WithdrawAsync(It.IsAny<long>(), It.IsAny<decimal>()));
            _portfolioServiceMock.Setup(p => p.DepositAsync(It.IsAny<decimal>(), It.IsAny<long>()));

            try
            {
                await _portfolioAppService.DepositAsync(amount, customerId, portfolioId);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            _customerBankInfoAppServiceMock.Verify(p => p.GetBalanceAsync(It.IsAny<long>()), Times.Once);
            _customerBankInfoAppServiceMock.Verify(p => p.WithdrawAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Never);
            _portfolioServiceMock.Verify(p => p.DepositAsync(It.IsAny<decimal>(), It.IsAny<long>()), Times.Never);
        }

        [Fact]
        public async Task Should_Withdraw_Sucessfully()
        {
            long portfolioId = 1;
            long customerId = 1;
            decimal amount = 100;
            decimal balance = 1000;

            _portfolioServiceMock.Setup(p => p.GetAccountBalanceAsync(It.IsAny<long>())).ReturnsAsync(balance);
            _portfolioServiceMock.Setup(p => p.WithdrawAsync(It.IsAny<decimal>(), It.IsAny<long>()));
            _customerBankInfoAppServiceMock.Setup(p => p.DepositAsync(It.IsAny<long>(), It.IsAny<decimal>()));

            await _portfolioAppService.WithdrawAsync(amount, customerId, portfolioId);

            _portfolioServiceMock.Verify(p => p.GetAccountBalanceAsync(It.IsAny<long>()), Times.Once);
            _portfolioServiceMock.Verify(p => p.WithdrawAsync(It.IsAny<decimal>(), It.IsAny<long>()), Times.Once);
            _customerBankInfoAppServiceMock.Verify(p => p.DepositAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
        }

        [Fact]
        public async void Should_Not_Withdraw_When_Balance_Is_Lesser_Than_Amount()
        {
            long portfolioId = 1;
            long customerId = 1;
            decimal amount = 100;
            decimal balance = 10;

            _portfolioServiceMock.Setup(p => p.GetAccountBalanceAsync(It.IsAny<long>())).ReturnsAsync(balance);
            _portfolioServiceMock.Setup(p => p.WithdrawAsync(It.IsAny<decimal>(), It.IsAny<long>()));
            _customerBankInfoAppServiceMock.Setup(p => p.DepositAsync(It.IsAny<long>(), It.IsAny<decimal>()));

            try
            {
                await _portfolioAppService.WithdrawAsync(amount, customerId, portfolioId);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            _portfolioServiceMock.Verify(p => p.GetAccountBalanceAsync(It.IsAny<long>()), Times.Once);
            _portfolioServiceMock.Verify(p => p.WithdrawAsync(It.IsAny<decimal>(), It.IsAny<long>()), Times.Never);
            _customerBankInfoAppServiceMock.Verify(p => p.DepositAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Never);
        }

        [Fact]
        public async void Should_InvestAsync_Sucessfully_With_Execute_Order()
        {
            int quotes = 1;
            DateTime liquidateAt = DateTime.Now.AddDays(-2);
            long productId = 1;
            long portfolioId = 1;
            var product = ProductResponseFixture.GenerateProductResponseFixture();
            product.UnitPrice = 0;
            var order = CreateOrderFixture.GenerateCreateOrderFixture();
            var orderResponse = OrderResponseFixture.GenerateOrderResponseFixture();
            var orderId = 1;

            _productAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(product);
            _orderAppServiceMock.Setup(p => p.CreateAsync(It.IsAny<CreateOrder>())).ReturnsAsync(orderId);
            _orderAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(orderResponse);

            await _portfolioAppService.InvestAsync(quotes, liquidateAt, productId, portfolioId);

            _productAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Exactly(2));
            _orderAppServiceMock.Verify(p => p.CreateAsync(It.IsAny<CreateOrder>()), Times.Once);
            _orderAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async void Should_InvestAsync_Sucessfully_Without_Execute_Order()
        {
            int quotes = 1;
            DateTime liquidateAt = DateTime.Now.AddDays(2);
            long productId = 1;
            long portfolioId = 1;
            var product = ProductResponseFixture.GenerateProductResponseFixture();
            product.UnitPrice = 0;
            var orderResponse = OrderResponseFixture.GenerateOrderResponseFixture();
            var orderId = 1;

            _productAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(product);
            _orderAppServiceMock.Setup(p => p.CreateAsync(It.IsAny<CreateOrder>())).ReturnsAsync(orderId);
            _orderAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(orderResponse);

            await _portfolioAppService.InvestAsync(quotes, liquidateAt, productId, portfolioId);

            _productAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _orderAppServiceMock.Verify(p => p.CreateAsync(It.IsAny<CreateOrder>()), Times.Once);
            _orderAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Never);
        }

        [Fact]
        public async void Should_Not_InvestAsync_When_Balance_Is_Lesser_Than_UnitPrice()
        {
            int quotes = 1;
            DateTime liquidateAt = DateTime.Now.AddDays(2);
            long productId = 1;
            long portfolioId = 1;
            var product = ProductResponseFixture.GenerateProductResponseFixture();
            product.UnitPrice = 10000;
            var orderResponse = OrderResponseFixture.GenerateOrderResponseFixture();
            var orderId = 1;

            _productAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(product);
            _orderAppServiceMock.Setup(p => p.CreateAsync(It.IsAny<CreateOrder>())).ReturnsAsync(orderId);
            _orderAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(orderResponse);
            try
            {
                await _portfolioAppService.InvestAsync(quotes, liquidateAt, productId, portfolioId);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            _productAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _orderAppServiceMock.Verify(p => p.CreateAsync(It.IsAny<CreateOrder>()), Times.Once);
            _orderAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Never);
        }

        [Fact]
        public async void Should_WithdrawProduct_Sucessfully_Without_Execute_OrderAsync()
        {
            int quotes = 1;
            DateTime liquidateAt = DateTime.Now.AddDays(-2);
            long productId = 1;
            long portfolioId = 1;
            var product = ProductResponseFixture.GenerateProductResponseFixture();
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            int avaliableQuotes = 2;
            long orderId = 1;
            var orderResponse = OrderResponseFixture.GenerateOrderResponseFixture();

            _productAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(product);
            _portfolioServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(portfolio);
            _orderAppServiceMock.Setup(p => p.GetQuotesAvaliableAsync(portfolioId, productId)).ReturnsAsync(avaliableQuotes);
            _orderAppServiceMock.Setup(p => p.CreateAsync(It.IsAny<CreateOrder>())).ReturnsAsync(orderId);
            _orderAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(orderResponse);

            await _portfolioAppService.WithdrawProductAsync(quotes, liquidateAt, productId, portfolioId);

            _productAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Exactly(2));
            _portfolioServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Exactly(2));
            _orderAppServiceMock.Verify(p => p.GetQuotesAvaliableAsync(portfolioId, productId), Times.Exactly(2));
            _orderAppServiceMock.Verify(p => p.CreateAsync(It.IsAny<CreateOrder>()), Times.Once);
            _orderAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async void Should_WithdrawProduct_Sucessfully_With_Execute_Order()
        {
            int quotes = 1;
            DateTime liquidateAt = DateTime.Now.AddDays(2);
            long productId = 1;
            long portfolioId = 1;
            var product = ProductResponseFixture.GenerateProductResponseFixture();
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            int avaliableQuotes = 2;
            long orderId = 1;
            var orderResponse = OrderResponseFixture.GenerateOrderResponseFixture();

            _productAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(product);
            _portfolioServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(portfolio);
            _orderAppServiceMock.Setup(p => p.GetQuotesAvaliableAsync(portfolioId, productId)).ReturnsAsync(avaliableQuotes);
            _orderAppServiceMock.Setup(p => p.CreateAsync(It.IsAny<CreateOrder>())).ReturnsAsync(orderId);
            _orderAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(orderResponse);

            await _portfolioAppService.WithdrawProductAsync(quotes, liquidateAt, productId, portfolioId);

            _productAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _portfolioServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _orderAppServiceMock.Verify(p => p.GetQuotesAvaliableAsync(portfolioId, productId), Times.Once);
            _orderAppServiceMock.Verify(p => p.CreateAsync(It.IsAny<CreateOrder>()), Times.Once);
            _orderAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Never);
        }

        [Fact]
        public async void Should_Not_WithdrawProduct_When_Quotes_Are_Unavaliable()
        {
            int quotes = 2;
            DateTime liquidateAt = DateTime.Now.AddDays(2);
            long productId = 1;
            long portfolioId = 1;
            var product = ProductResponseFixture.GenerateProductResponseFixture();
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            int avaliableQuotes = 1;
            long orderId = 1;
            var orderResponse = OrderResponseFixture.GenerateOrderResponseFixture();

            _productAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(product);
            _portfolioServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(portfolio);
            _orderAppServiceMock.Setup(p => p.GetQuotesAvaliableAsync(portfolioId, productId)).ReturnsAsync(avaliableQuotes);
            _orderAppServiceMock.Setup(p => p.CreateAsync(It.IsAny<CreateOrder>())).ReturnsAsync(orderId);
            _orderAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(orderResponse);

            try
            {
                await _portfolioAppService.WithdrawProductAsync(quotes, liquidateAt, productId, portfolioId);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            _productAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _portfolioServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _orderAppServiceMock.Verify(p => p.GetQuotesAvaliableAsync(portfolioId, productId), Times.Once);
            _orderAppServiceMock.Verify(p => p.CreateAsync(It.IsAny<CreateOrder>()), Times.Never);
            _orderAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Never);
        }

        [Fact]
        public void Should_ExecuteNowOrdersAsync_Sucessfully_With_Sell_And_Buy_Orders()
        {
            var orders = OrderResponseFixture.GenerateOrderResponseFixture(4);
            orders[0].Direction = OrderDirection.Sell;
            orders[1].Direction = OrderDirection.Buy;
            orders[2].Direction = OrderDirection.Sell;
            orders[3].Direction = OrderDirection.Buy;

            _orderAppServiceMock.Setup(p => p.GetExecutableOrdersAsync()).ReturnsAsync(orders);

            _ = _portfolioAppService.ExecuteNowOrdersAsync();

            _orderAppServiceMock.Verify(p => p.GetExecutableOrdersAsync(), Times.Once);
        }

        [Fact]
        public async void Should_ExecuteBuyOrderAsync_Sucessfully_And_Creating_Relation()
        {
            var orderResponse = OrderResponseFixture.GenerateOrderResponseFixture();
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            var productResponse = ProductResponseFixture.GenerateProductResponseFixture();
            var product = ProductFixture.GenerateProductFixture();

            _portfolioServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(portfolio);
            _productAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(productResponse);
            _portfolioServiceMock.Setup(p => p.ExecuteBuyOrderAsync(It.IsAny<decimal>(), It.IsAny<long>()));
            _portfolioProductServiceMock.Setup(p => p.RelationAlreadyExistsAsync(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(false);
            _portfolioProductServiceMock.Setup(p => p.InitRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()));

            await _portfolioAppService.ExecuteBuyOrderAsync(orderResponse);

            _portfolioServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _productAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _portfolioServiceMock.Verify(p => p.ExecuteBuyOrderAsync(It.IsAny<decimal>(), It.IsAny<long>()), Times.Once);
            _portfolioProductServiceMock.Verify(p => p.RelationAlreadyExistsAsync(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
            _portfolioProductServiceMock.Verify(p => p.InitRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async void Should_ExecuteBuyOrderAsync_Sucessfully_And_Not_Creating_Relation()
        {
            var orderResponse = OrderResponseFixture.GenerateOrderResponseFixture();
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            var productResponse = ProductResponseFixture.GenerateProductResponseFixture();
            var product = ProductFixture.GenerateProductFixture();

            _portfolioServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(portfolio);
            _productAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(productResponse);
            _portfolioServiceMock.Setup(p => p.ExecuteBuyOrderAsync(It.IsAny<decimal>(), It.IsAny<long>()));
            _portfolioProductServiceMock.Setup(p => p.RelationAlreadyExistsAsync(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(true);
            _portfolioProductServiceMock.Setup(p => p.InitRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()));

            await _portfolioAppService.ExecuteBuyOrderAsync(orderResponse);

            _portfolioServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _productAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _portfolioServiceMock.Verify(p => p.ExecuteBuyOrderAsync(It.IsAny<decimal>(), It.IsAny<long>()), Times.Once);
            _portfolioProductServiceMock.Verify(p => p.RelationAlreadyExistsAsync(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
            _portfolioProductServiceMock.Verify(p => p.InitRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async void Should_ExecuteSellOrderAsync_Sucessfully_And_Disposing_Relation()
        {
            var orderResponse = OrderResponseFixture.GenerateOrderResponseFixture();
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            var productResponse = ProductResponseFixture.GenerateProductResponseFixture();
            var product = ProductFixture.GenerateProductFixture();
            var quotesAvaliable = 0;

            _portfolioServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(portfolio);
            _productAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(productResponse);
            _portfolioServiceMock.Setup(p => p.ExecuteSellOrderAsync(It.IsAny<decimal>(), It.IsAny<long>()));
            _orderAppServiceMock.Setup(p => p.GetQuotesAvaliableAsync(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(quotesAvaliable);
            _portfolioProductServiceMock.Setup(p => p.DisposeRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()));

            await _portfolioAppService.ExecuteSellOrderAsync(orderResponse);

            _portfolioServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _productAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _portfolioServiceMock.Verify(p => p.ExecuteSellOrderAsync(It.IsAny<decimal>(), It.IsAny<long>()), Times.Once);
            _orderAppServiceMock.Verify(p => p.GetQuotesAvaliableAsync(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
            _portfolioProductServiceMock.Verify(p => p.DisposeRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async void Should_ExecuteSellOrderAsyncSucessfully_And_Not_Disposing_Relation()
        {
            var orderResponse = OrderResponseFixture.GenerateOrderResponseFixture();
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            var productResponse = ProductResponseFixture.GenerateProductResponseFixture();
            var product = ProductFixture.GenerateProductFixture();
            var quotesAvaliable = 3;

            _portfolioServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(portfolio);
            _productAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(productResponse);
            _portfolioServiceMock.Setup(p => p.ExecuteSellOrderAsync(It.IsAny<decimal>(), It.IsAny<long>()));
            _orderAppServiceMock.Setup(p => p.GetQuotesAvaliableAsync(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(quotesAvaliable);
            _portfolioProductServiceMock.Setup(p => p.DisposeRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()));

            await _portfolioAppService.ExecuteSellOrderAsync(orderResponse);

            _portfolioServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _productAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _portfolioServiceMock.Verify(p => p.ExecuteSellOrderAsync(It.IsAny<decimal>(), It.IsAny<long>()), Times.Once);
            _orderAppServiceMock.Verify(p => p.GetQuotesAvaliableAsync(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
            _portfolioProductServiceMock.Verify(p => p.DisposeRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async void Should_Delete_Sucessfully()
        {
            long portfolioId = 1;

            _portfolioServiceMock.Setup(p => p.DeleteAsync(It.IsAny<long>()));

            await _portfolioAppService.DeleteAsync(portfolioId);

            _portfolioServiceMock.Verify(p => p.DeleteAsync(It.IsAny<long>()), Times.Once);
        }
    }
}
