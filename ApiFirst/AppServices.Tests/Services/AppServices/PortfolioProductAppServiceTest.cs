using ApiFirst.Tests.Fixtures.DomainServices;
using AppServices.Services;
using DomainModels.Models;
using DomainServices.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiFirst.Tests.Services.AppServices
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
        public async void Should_InitRelation_Sucessfully()
        {
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            var product = ProductFixture.GenerateProductFixture();

            _portfolioProductServiceMock.Setup(p => p.InitRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()));

            await _portfolioProductAppService.InitRelationAsync(portfolio, product);

            _portfolioProductServiceMock.Verify(p => p.InitRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async void Should_DisposeRelation_Sucessfully()
        {
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            var product = ProductFixture.GenerateProductFixture();

            _portfolioProductServiceMock.Setup(p => p.DisposeRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()));

            await _portfolioProductAppService.DisposeRelationAsync(portfolio, product);

            _portfolioProductServiceMock.Verify(p => p.DisposeRelationAsync(It.IsAny<Portfolio>(), It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async void Should_RelationAlreadyExistsAsync_Sucessfully()
        {
            long portfolioId = 1;
            long productId = 1;

            _portfolioProductServiceMock.Setup(p => p.RelationAlreadyExistsAsync(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(true);

            var result = await _portfolioProductAppService.RelationAlreadyExistsAsync(portfolioId, productId);

            result.Should().BeTrue();

            _portfolioProductServiceMock.Verify(p => p.RelationAlreadyExistsAsync(It.IsAny<long>(), It.IsAny<long>()));
        }

        [Fact]
        public async void Should_RelationAlreadyExists_Sucessfully_From_False()
        {
            long portfolioId = 1;
            long productId = 1;

            _portfolioProductServiceMock.Setup(p => p.RelationAlreadyExistsAsync(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(false);

            var result = await _portfolioProductAppService.RelationAlreadyExistsAsync(portfolioId, productId);

            result.Should().BeFalse();

            _portfolioProductServiceMock.Verify(p => p.RelationAlreadyExistsAsync(It.IsAny<long>(), It.IsAny<long>()));
        }
    }
}
