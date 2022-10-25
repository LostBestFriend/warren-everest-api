using AppModels.AppModels.Orders;
using AppModels.EnumModels;
using AppServices.Interfaces;
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
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ICustomerBankInfoAppService> _customerBankInfoAppServiceMock;
        private readonly Mock<IProductAppService> _productAppServiceMock;
        private readonly Mock<IOrderAppService> _orderAppServiceMock;
        private readonly Mock<IPortfolioProductService> _portfolioProductServiceMock;
        private readonly PortfolioAppService _portfolioAppService;

        public PortfolioAppServiceTest()
        {
            _portfolioServiceMock = new();
            _mapperMock = new();
            _customerBankInfoAppServiceMock = new();
            _productAppServiceMock = new();
            _orderAppServiceMock = new();
            _portfolioProductServiceMock = new();
            _portfolioAppService = new PortfolioAppService(portfolio: _portfolioServiceMock.Object, mapper: _mapperMock.Object, customerBankInfoAppServices: _customerBankInfoAppServiceMock.Object, productAppServices: _productAppServiceMock.Object, orderAppServices: _orderAppServiceMock.Object, portfolioProductServices: _portfolioProductServiceMock.Object);
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
        public async Task Should_GetAll_SucessfullyAsync()
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
        public void Should_Deposit_Sucessfully()
        {
            long portfolioId = 1;
            long customerId = 1;
            decimal amount = 100;
            decimal balance = 1000;

            _customerBankInfoAppServiceMock.Setup(p => p.GetBalance(It.IsAny<long>())).Returns(balance);
            _customerBankInfoAppServiceMock.Setup(p => p.Withdraw(It.IsAny<long>(), It.IsAny<decimal>()));
            _portfolioServiceMock.Setup(p => p.DepositAsync(It.IsAny<decimal>(), It.IsAny<long>()));

            _portfolioAppService.Deposit(amount, customerId, portfolioId);

            _customerBankInfoAppServiceMock.Verify(p => p.GetBalance(It.IsAny<long>()), Times.Once);
            _customerBankInfoAppServiceMock.Verify(p => p.Withdraw(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
            _portfolioServiceMock.Verify(p => p.DepositAsync(It.IsAny<decimal>(), It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public void Should_Withdraw_Sucessfully()
        {
            long portfolioId = 1;
            long customerId = 1;
            decimal amount = 100;
            decimal balance = 1000;

            _portfolioServiceMock.Setup(p => p.GetAccountBalanceAsync(It.IsAny<long>())).ReturnsAsync(balance);
            _portfolioServiceMock.Setup(p => p.WithdrawAsync(It.IsAny<decimal>(), It.IsAny<long>()));
            _customerBankInfoAppServiceMock.Setup(p => p.Deposit(It.IsAny<long>(), It.IsAny<decimal>()));

            _portfolioAppService.WithdrawAsync(amount, customerId, portfolioId);

            _portfolioServiceMock.Verify(p => p.GetAccountBalanceAsync(It.IsAny<long>()), Times.Once);
            _portfolioServiceMock.Verify(p => p.WithdrawAsync(It.IsAny<decimal>(), It.IsAny<long>()), Times.Once);
            _customerBankInfoAppServiceMock.Verify(p => p.Deposit(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
        }

        [Fact]
        public void Should_InvestAsync_Sucessfully_With_Execute_Order()
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

            _portfolioAppService.InvestAsync(quotes, liquidateAt, productId, portfolioId);

            _productAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Exactly(2));
            _orderAppServiceMock.Verify(p => p.CreateAsync(It.IsAny<CreateOrder>()), Times.Once);
            _orderAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public void Should_InvestAsync_Sucessfully_Without_Execute_Order()
        {
            int quotes = 1;
            DateTime liquidateAt = DateTime.Now.AddDays(-2);
            long productId = 1;
            long portfolioId = 1;
            var product = ProductResponseFixture.GenerateProductResponseFixture();
            var orderResponse = OrderResponseFixture.GenerateOrderResponseFixture();
            var orderId = 1;

            _productAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(product);
            _orderAppServiceMock.Setup(p => p.CreateAsync(It.IsAny<CreateOrder>())).ReturnsAsync(orderId);
            _orderAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(orderResponse);

            _portfolioAppService.InvestAsync(quotes, liquidateAt, productId, portfolioId);

            _productAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _orderAppServiceMock.Verify(p => p.CreateAsync(It.IsAny<CreateOrder>()), Times.Once);
            _orderAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Never);
        }

        [Fact]
        public void Should_WithdrawProduct_Sucessfully_Without_Execute_Order()
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
            _orderAppServiceMock.Setup(p => p.GetQuotesAvaliable(portfolioId, productId)).Returns(avaliableQuotes);
            _orderAppServiceMock.Setup(p => p.CreateAsync(It.IsAny<CreateOrder>())).ReturnsAsync(orderId);
            _orderAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(orderResponse);

            _portfolioAppService.WithdrawProduct(quotes, liquidateAt, productId, portfolioId);

            _productAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Exactly(2));
            _portfolioServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Exactly(2));
            _orderAppServiceMock.Verify(p => p.GetQuotesAvaliable(portfolioId, productId), Times.Exactly(2));
            _orderAppServiceMock.Verify(p => p.CreateAsync(It.IsAny<CreateOrder>()), Times.Once);
            _orderAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public void Should_WithdrawProduct_Sucessfully_With_Execute_Order()
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
            _orderAppServiceMock.Setup(p => p.GetQuotesAvaliable(portfolioId, productId)).Returns(avaliableQuotes);
            _orderAppServiceMock.Setup(p => p.CreateAsync(It.IsAny<CreateOrder>())).ReturnsAsync(orderId);
            _orderAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(orderResponse);

            _portfolioAppService.WithdrawProduct(quotes, liquidateAt, productId, portfolioId);

            _productAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _portfolioServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _orderAppServiceMock.Verify(p => p.GetQuotesAvaliable(portfolioId, productId), Times.Once);
            _orderAppServiceMock.Verify(p => p.CreateAsync(It.IsAny<CreateOrder>()), Times.Once);
            _orderAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Never);
        }

        [Fact]
        public void Should_Not_WithdrawProduct_When_Quotes_Are_Unavaliable()
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
            _orderAppServiceMock.Setup(p => p.GetQuotesAvaliable(portfolioId, productId)).Returns(avaliableQuotes);
            _orderAppServiceMock.Setup(p => p.CreateAsync(It.IsAny<CreateOrder>())).ReturnsAsync(orderId);
            _orderAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(orderResponse);

            _portfolioAppService.WithdrawProduct(quotes, liquidateAt, productId, portfolioId);

            _productAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _portfolioServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _orderAppServiceMock.Verify(p => p.GetQuotesAvaliable(portfolioId, productId), Times.Once);
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
        public void Should_ExecuteBuyOrderAsyncSucessfully_And_Creating_Relation()
        {
            var orderResponse = OrderResponseFixture.GenerateOrderResponseFixture();
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            var productResponse = ProductResponseFixture.GenerateProductResponseFixture();
            var product = ProductFixture.GenerateProductFixture();

            _portfolioServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(portfolio);
            _productAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(productResponse);
            _portfolioServiceMock.Setup(p => p.ExecuteBuyOrderAsync(It.IsAny<decimal>(), It.IsAny<long>()));
            _portfolioProductServiceMock.Setup(p => p.RelationAlreadyExists(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(false);
            _portfolioProductServiceMock.Setup(p => p.InitRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()));

            _portfolioAppService.ExecuteBuyOrderAsync(orderResponse);

            _portfolioServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _productAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _portfolioServiceMock.Verify(p => p.ExecuteBuyOrderAsync(It.IsAny<decimal>(), It.IsAny<long>()), Times.Once);
            _portfolioProductServiceMock.Verify(p => p.RelationAlreadyExists(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
            _portfolioProductServiceMock.Verify(p => p.InitRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void Should_ExecuteBuyOrderAsyncSucessfully_And_Not_Creating_Relation()
        {
            var orderResponse = OrderResponseFixture.GenerateOrderResponseFixture();
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            var productResponse = ProductResponseFixture.GenerateProductResponseFixture();
            var product = ProductFixture.GenerateProductFixture();

            _portfolioServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(portfolio);
            _productAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(productResponse);
            _portfolioServiceMock.Setup(p => p.ExecuteBuyOrderAsync(It.IsAny<decimal>(), It.IsAny<long>()));
            _portfolioProductServiceMock.Setup(p => p.RelationAlreadyExists(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(true);
            _portfolioProductServiceMock.Setup(p => p.InitRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()));

            _portfolioAppService.ExecuteBuyOrderAsync(orderResponse);

            _portfolioServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _productAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _portfolioServiceMock.Verify(p => p.ExecuteBuyOrderAsync(It.IsAny<decimal>(), It.IsAny<long>()), Times.Once);
            _portfolioProductServiceMock.Verify(p => p.RelationAlreadyExists(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
            _portfolioProductServiceMock.Verify(p => p.InitRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public void Should_ExecuteSellOrderAsyncSucessfully_And_Disposing_Relation()
        {
            var orderResponse = OrderResponseFixture.GenerateOrderResponseFixture();
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            var productResponse = ProductResponseFixture.GenerateProductResponseFixture();
            var product = ProductFixture.GenerateProductFixture();
            var quotesAvaliable = 0;

            _portfolioServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(portfolio);
            _productAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(productResponse);
            _portfolioServiceMock.Setup(p => p.ExecuteSellOrderAsync(It.IsAny<decimal>(), It.IsAny<long>()));
            _orderAppServiceMock.Setup(p => p.GetQuotesAvaliable(It.IsAny<long>(), It.IsAny<long>())).Returns(quotesAvaliable);
            _portfolioProductServiceMock.Setup(p => p.DisposeRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()));

            _portfolioAppService.ExecuteSellOrderAsync(orderResponse);

            _portfolioServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _productAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _portfolioServiceMock.Verify(p => p.ExecuteSellOrderAsync(It.IsAny<decimal>(), It.IsAny<long>()), Times.Once);
            _orderAppServiceMock.Verify(p => p.GetQuotesAvaliable(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
            _portfolioProductServiceMock.Verify(p => p.DisposeRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void Should_ExecuteSellOrderAsyncSucessfully_And_Not_Disposing_Relation()
        {
            var orderResponse = OrderResponseFixture.GenerateOrderResponseFixture();
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            var productResponse = ProductResponseFixture.GenerateProductResponseFixture();
            var product = ProductFixture.GenerateProductFixture();
            var quotesAvaliable = 3;

            _portfolioServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(portfolio);
            _productAppServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(productResponse);
            _portfolioServiceMock.Setup(p => p.ExecuteSellOrderAsync(It.IsAny<decimal>(), It.IsAny<long>()));
            _orderAppServiceMock.Setup(p => p.GetQuotesAvaliable(It.IsAny<long>(), It.IsAny<long>())).Returns(quotesAvaliable);
            _portfolioProductServiceMock.Setup(p => p.DisposeRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()));

            _portfolioAppService.ExecuteSellOrderAsync(orderResponse);

            _portfolioServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _productAppServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _portfolioServiceMock.Verify(p => p.ExecuteSellOrderAsync(It.IsAny<decimal>(), It.IsAny<long>()), Times.Once);
            _orderAppServiceMock.Verify(p => p.GetQuotesAvaliable(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
            _portfolioProductServiceMock.Verify(p => p.DisposeRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public void Should_Delete_Sucessfully()
        {
            long portfolioId = 1;

            _portfolioServiceMock.Setup(p => p.DeleteAsync(It.IsAny<long>()));

            _portfolioAppService.Delete(portfolioId);

            _portfolioServiceMock.Verify(p => p.DeleteAsync(It.IsAny<long>()), Times.Once);
        }
    }
}
