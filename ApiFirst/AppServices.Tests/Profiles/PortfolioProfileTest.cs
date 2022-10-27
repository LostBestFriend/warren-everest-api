using AppModels.AppModels.Portfolios;
using AppServices.Profiles;
using AppServices.Tests.Fixtures.Customer;
using AppServices.Tests.Fixtures.Order;
using AppServices.Tests.Fixtures.PortfolioProduct;
using AutoMapper;
using DomainModels.Models;
using FluentAssertions;
using Xunit;

namespace AppServices.Tests.Profiles
{
    public class PortfolioProfileTest
    {
        private readonly IMapper _mapper;

        public PortfolioProfileTest()
        {
            _mapper = new MapperConfiguration(cfg =>
            cfg.AddProfile<PortfolioProfile>()).CreateMapper();
        }

        [Fact]
        public void Should_Map_UpdatePortfolio_Sucessfully()
        {
            var portfolio = new Portfolio(name: "João", description:
                "aaa", totalBalance: 1000, customerId: 1);

            var updatePortfolio = new UpdatePortfolio(name: "João",
                description: "aaa", customerId: 1);

            var result = _mapper.Map<Portfolio>(updatePortfolio);

            result.Should().NotBeNull();
        }

        [Fact]
        public void Should_Map_CreatePortfolio_Sucessfully()
        {
            var portfolio = new Portfolio(name: "João", description:
                "aaa", totalBalance: 1000, customerId: 1);
            var createPortfolio = new CreatePortfolio(name: "João",
                description: "aaa", customerId: 1);

            var result = _mapper.Map<Portfolio>(createPortfolio);

            result.Should().NotBeNull();
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

            var result = _mapper.Map<PortfolioResponse>(portfolio);

            result.Should().NotBeNull();
        }
    }
}
