using AppModels.AppModels.Portfolios;
using AppServices.Tests.Fixtures.Customer;
using AppServices.Tests.Fixtures.Order;
using AppServices.Tests.Fixtures.PortfolioProduct;
using AutoMapper;
using DomainModels.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace AppServices.Tests.Profiles
{
    public class PortfolioProfileTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IMapper> _mapperMock;

        public PortfolioProfileTest()
        {
            _mapperMock = new();
            _mapper = _mapperMock.Object;
        }

        [Fact]
        public void Should_Map_UpdatePortfolio_Sucessfully()
        {
            var portfolio = new Portfolio(name: "João", description:
                "aaa", totalBalance: 1000, customerId: 1);

            var updatePortfolio = new UpdatePortfolio(name: "João",
                description: "aaa", customerId: 1);

            _mapperMock.Setup(p => p.Map<Portfolio>(It.IsAny<UpdatePortfolio>())).Returns(portfolio);

            var result = _mapper.Map<Portfolio>(updatePortfolio);

            result.Should().BeEquivalentTo(portfolio);

            _mapperMock.Verify(p => p.Map<Portfolio>(It.IsAny<UpdatePortfolio>()), Times.Once);
        }

        [Fact]
        public void Should_Map_CreatePortfolio_Sucessfully()
        {
            var portfolio = new Portfolio(name: "João", description:
                "aaa", totalBalance: 1000, customerId: 1);
            var createPortfolio = new CreatePortfolio(name: "João",
                description: "aaa", customerId: 1);

            _mapperMock.Setup(p => p.Map<Portfolio>(It.IsAny<CreatePortfolio>())).Returns(portfolio);

            var result = _mapper.Map<Portfolio>(createPortfolio);

            _mapperMock.Verify(p => p.Map<Portfolio>(It.IsAny<CreatePortfolio>()), Times.Once);

            result.Should().BeEquivalentTo(portfolio);
        }

        [Fact]
        public void Should_Map_PortfolioResponse_Sucessfully()
        {
            var portfolio = new Portfolio(name: "João", description:
                "aaa", totalBalance: 1000, customerId: 1);
            var portfolioResponse = new PortfolioResponse(id: 1,
                name: "João", description: "aaa", totalBalance: 1000,
                accountBalance: 1000,
                customer: CustomerResponseFixture.GenerateCustomerResponseFixture(),
                products: PortfolioProductResponseFixture.GeneratePortfolioProductResponseFixture(3),
                orders: OrderResponseFixture.GenerateOrderResponseFixture(3));

            _mapperMock.Setup(p => p.Map<PortfolioResponse>(It.IsAny<Portfolio>())).Returns(portfolioResponse);

            var result = _mapper.Map<PortfolioResponse>(portfolio);

            result.Should().BeEquivalentTo(portfolioResponse);

            _mapperMock.Verify(p => p.Map<PortfolioResponse>(It.IsAny<Portfolio>()), Times.Once);
        }
    }
}
