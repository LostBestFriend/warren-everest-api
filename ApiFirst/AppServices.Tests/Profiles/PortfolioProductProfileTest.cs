using AppModels.AppModels.PortfolioProducts;
using AppServices.Tests.Fixtures.Product;
using AutoMapper;
using DomainModels.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace AppServices.Tests.Profiles
{
    public class PortfolioProductProfileTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IMapper> _mapperMock;

        public PortfolioProductProfileTest()
        {
            _mapperMock = new();
            _mapper = _mapperMock.Object;
        }
        [Fact]
        public void Should_Map_PortfolioProductResponse_Sucessfully()
        {
            var portfolioProduct = new PortfolioProduct(
                productId: 1, portfolioId: 1);
            var portfolioProductResponse = new PortfolioProductResponse(
                product: ProductResponseFixture.GenerateProductResponseFixture());

            _mapperMock.Setup(p => p.Map<PortfolioProductResponse>(It.IsAny<PortfolioProduct>())).Returns(portfolioProductResponse);

            var result = _mapper.Map<PortfolioProductResponse>(portfolioProduct);

            result.Should().BeEquivalentTo(portfolioProductResponse);

            _mapperMock.Verify(p => p.Map<PortfolioProductResponse>(It.IsAny<PortfolioProduct>()), Times.Once);
        }
    }
}
