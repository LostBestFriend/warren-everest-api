using ApiFirst.Tests.Fixtures.DomainServices;
using AppModels.AppModels.PortfolioProducts;
using AppServices.Profiles;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace ApiFirst.Tests.Profiles
{
    public class PortfolioProductProfileTest
    {
        private readonly IMapper _mapperResponse;

        public PortfolioProductProfileTest()
        {
            _mapperResponse = new MapperConfiguration(cfg => cfg.AddProfile<PortfolioProductProfile>()).CreateMapper();
        }
        [Fact]
        public void Should_Map_PortfolioProductResponse_Sucessfully()
        {
            var portfolioProduct = PortfolioProductFixture.GeneratePortfolioProductFixture();

            var result = _mapperResponse.Map<PortfolioProductResponse>(portfolioProduct);

            result.Should().NotBeNull();
        }
    }
}
