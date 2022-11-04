using AppModels.AppModels.Portfolios;
using AppServices.Profiles;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Tests.Fixtures;
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
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();

            var updatePortfolio = new UpdatePortfolio(
                name: portfolio.Name,
                description: portfolio.Description,
                customerId: portfolio.CustomerId);

            var result = _mapper.Map<Portfolio>(updatePortfolio);

            result.Should().NotBeNull();
        }

        [Fact]
        public void Should_Map_CreatePortfolio_Sucessfully()
        {
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();

            var createPortfolio = new CreatePortfolio(
                name: portfolio.Name,
                description: portfolio.Description,
                customerId: portfolio.CustomerId);

            var result = _mapper.Map<Portfolio>(createPortfolio);

            result.Should().NotBeNull();
        }

        [Fact]
        public void Should_Map_PortfolioResponse_Sucessfully()
        {
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();

            var result = _mapper.Map<PortfolioResponse>(portfolio);

            result.Should().NotBeNull();
        }
    }
}
