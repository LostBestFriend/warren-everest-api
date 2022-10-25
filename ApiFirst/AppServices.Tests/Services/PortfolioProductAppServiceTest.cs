using AppServices.Services;
using DomainModels.Models;
using DomainServices.Interfaces;
using DomainServices.Tests.Fixtures;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace AppServices.Tests.Services
{
    public class PortfolioProductAppServiceTest
    {
        readonly Mock<IPortfolioProductService> _portfolioProductServiceMock;
        readonly PortfolioProductAppService _portfolioProductAppService;

        public PortfolioProductAppServiceTest()
        {
            _portfolioProductServiceMock = new();
            _portfolioProductAppService = new PortfolioProductAppService(_portfolioProductServiceMock.Object);
        }

        [Fact]
        public void Should_InitRelation_Sucessfully()
        {
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            var product = ProductFixture.GenerateProductFixture();

            _portfolioProductServiceMock.Setup(p => p.InitRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()));

            _ = _portfolioProductAppService.InitRelationAsync(portfolio, product);

            _portfolioProductServiceMock.Verify(p => p.InitRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void Should_DisposeRelation_Sucessfully()
        {
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            var product = ProductFixture.GenerateProductFixture();

            _portfolioProductServiceMock.Setup(p => p.DisposeRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()));

            _portfolioProductAppService.DisposeRelation(portfolio, product);

            _portfolioProductServiceMock.Verify(p => p.DisposeRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task Should_RelationAlreadyExistsAsync_Sucessfully()
        {
            long portfolioId = 1;
            long productId = 1;

            _portfolioProductServiceMock.Setup(p => p.RelationAlreadyExists(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(true);

            var result = await _portfolioProductAppService.RelationAlreadyExists(portfolioId, productId);
            result.Should().BeTrue();

            _portfolioProductServiceMock.Verify(p => p.RelationAlreadyExists(It.IsAny<long>(), It.IsAny<long>()));
        }

        [Fact]
        public async void Should_RelationAlreadyExists_Sucessfully_From_False()
        {
            long portfolioId = 1;
            long productId = 1;

            _portfolioProductServiceMock.Setup(p => p.RelationAlreadyExists(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(false);

            var result = await _portfolioProductAppService.RelationAlreadyExists(portfolioId, productId);
            result.Should().BeFalse();

            _portfolioProductServiceMock.Verify(p => p.RelationAlreadyExists(It.IsAny<long>(), It.IsAny<long>()));
        }
    }
}
