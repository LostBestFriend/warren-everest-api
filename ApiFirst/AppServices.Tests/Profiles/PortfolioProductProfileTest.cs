using AppModels.AppModels.PortfolioProducts;
using AppServices.Profiles;
using AppServices.Tests.Fixtures.Product;
using AutoMapper;
using DomainModels.Models;
using FluentAssertions;
using Xunit;

namespace AppServices.Tests.Profiles
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
            var portfolioProduct = new PortfolioProduct(
                productId: 1, portfolioId: 1);
            var portfolioProductResponse = new PortfolioProductResponse(
                product: ProductResponseFixture.GenerateProductResponseFixture());

            var result = _mapperResponse.Map<PortfolioProductResponse>(portfolioProduct);

            result.Should().NotBeNull();
        }
    }
}
