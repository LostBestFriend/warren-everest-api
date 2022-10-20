using DomainModels.Models;
using DomainServices.Services;
using DomainServices.Tests.Fixtures;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Infrastructure.Data.Context;
using Moq;
using Xunit;

namespace DomainServices.Tests.Services
{
    public class PortfolioProductServiceTest
    {
        readonly PortfolioProductService _portfolioProductService;
        readonly IUnitOfWork<WarrenContext> _unitOfWork;
        readonly IRepositoryFactory<WarrenContext> _repositoryFactory;
        readonly Mock<IRepositoryFactory<WarrenContext>> _repositoryFactoryMock;
        readonly Mock<IUnitOfWork<WarrenContext>> _unitOfWorkMock;

        public PortfolioProductServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork<WarrenContext>>();
            _unitOfWork = _unitOfWorkMock.Object;
            _repositoryFactoryMock = new Mock<IRepositoryFactory<WarrenContext>>();
            _repositoryFactory = _repositoryFactoryMock.Object;

            _portfolioProductService = new PortfolioProductService(_unitOfWork, _repositoryFactory);
        }

        [Fact]
        public async void Should_InitRelationAsync_Sucessfully()
        {
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            var product = ProductFixture.GenerateProductFixture();

            _unitOfWorkMock.Setup(p => p.Repository<PortfolioProduct>().AddAsync(It.IsAny<PortfolioProduct>(), default));
            _unitOfWorkMock.Setup(p => p.SaveChangesAsync(true, false, default));

            await _portfolioProductService.InitRelationAsync(portfolio, product);

            _unitOfWorkMock.Verify(p => p.Repository<PortfolioProduct>().AddAsync(It.IsAny<PortfolioProduct>(), default), Times.Once);
            _unitOfWorkMock.Verify(p => p.SaveChangesAsync(true, false, default), Times.Once);
        }
    }
}
