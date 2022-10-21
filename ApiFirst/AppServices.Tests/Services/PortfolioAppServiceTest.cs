using AppServices.Interfaces;
using AppServices.Services;
using AppServices.Tests.Fixtures.Portfolio;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using DomainServices.Tests.Fixtures;
using FluentAssertions;
using Moq;
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
        public void Should_GetAll_Sucessfully()
        {
            var portfolios = PortfolioFixture.GeneratePortfolioFixture(3);
            var portfolioResponses = PortfolioResponseFixture.GeneratePortfolioResponseFixture(3);

            _portfolioServiceMock.Setup(p => p.GetAll()).Returns(portfolios);

            var result = _portfolioAppService.GetAll();

            result.Should().NotBeNull();

            _portfolioServiceMock.Verify(p => p.GetAll(), Times.Once);
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
        public void Should_GetAccountBalance_Sucessfully()
        {
            long portfolioId = 1;

            _portfolioServiceMock.Setup(p => p.GetAccountBalance(It.IsAny<long>())).Returns(It.IsAny<decimal>());

            var result = _portfolioAppService.GetAccountBalance(portfolioId);

            result.Should().BeGreaterThanOrEqualTo(0);

            _portfolioServiceMock.Verify(p => p.GetAccountBalance(It.IsAny<long>()), Times.Once);
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
            _portfolioServiceMock.Setup(p => p.Deposit(It.IsAny<decimal>(), It.IsAny<long>()));

            _portfolioAppService.Deposit(amount, customerId, portfolioId);

            _customerBankInfoAppServiceMock.Verify(p => p.GetBalance(It.IsAny<long>()), Times.Once);
            _customerBankInfoAppServiceMock.Verify(p => p.Withdraw(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
            _portfolioServiceMock.Verify(p => p.Deposit(It.IsAny<decimal>(), It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public void Should_Withdraw_Sucessfully()
        {
            long portfolioId = 1;
            long customerId = 1;
            decimal amount = 100;
            decimal balance = 1000;

            _portfolioServiceMock.Setup(p => p.GetAccountBalance(It.IsAny<long>())).Returns(balance);
            _portfolioServiceMock.Setup(p => p.Withdraw(It.IsAny<decimal>(), It.IsAny<long>()));
            _customerBankInfoAppServiceMock.Setup(p => p.Deposit(It.IsAny<long>(), It.IsAny<decimal>()));

            _portfolioAppService.Withdraw(amount, customerId, portfolioId);

            _portfolioServiceMock.Verify(p => p.GetAccountBalance(It.IsAny<long>()), Times.Once);
            _portfolioServiceMock.Verify(p => p.Withdraw(It.IsAny<decimal>(), It.IsAny<long>()), Times.Once);
            _customerBankInfoAppServiceMock.Verify(p => p.Deposit(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
        }

    }
}
