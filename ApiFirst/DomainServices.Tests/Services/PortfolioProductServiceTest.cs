using DomainModels.Models;
using DomainServices.Services;
using DomainServices.Tests.Fixtures;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentAssertions;
using Infrastructure.Data.Context;
using Moq;
using System;
using System.Linq.Expressions;
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

        [Fact]
        public async void Should_DisposeRelationAsync_Sucessfully()
        {
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            var product = ProductFixture.GenerateProductFixture();
            var portfolioProduct = PortfolioProductFixture.GeneratePortfolioProductFixture();

            _repositoryFactoryMock.Setup(p => p.Repository<PortfolioProduct>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<PortfolioProduct, bool>>>())).Returns(It.IsAny<IQuery<PortfolioProduct>>());
            _repositoryFactoryMock.Setup(p => p.Repository<PortfolioProduct>().FirstOrDefaultAsync(It.IsAny<IQuery<PortfolioProduct>>(), default)).ReturnsAsync(portfolioProduct);
            _unitOfWorkMock.Setup(p => p.Repository<PortfolioProduct>().Remove(It.IsAny<PortfolioProduct>()));
            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            await _portfolioProductService.DisposeRelationAsync(portfolio, product);

            _repositoryFactoryMock.Verify(p => p.Repository<PortfolioProduct>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<PortfolioProduct, bool>>>()), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<PortfolioProduct>().FirstOrDefaultAsync(It.IsAny<IQuery<PortfolioProduct>>(), default), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<PortfolioProduct>().Remove(It.IsAny<PortfolioProduct>()), Times.Once);
            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Once);
        }

        [Fact]
        public async void Should_GetByRelationAsync_Sucessfully()
        {
            long portfolioId = 1;
            long productId = 1;
            var portfolioProduct = PortfolioProductFixture.GeneratePortfolioProductFixture();

            _repositoryFactoryMock.Setup(p => p.Repository<PortfolioProduct>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<PortfolioProduct, bool>>>())).Returns(It.IsAny<IQuery<PortfolioProduct>>());
            _repositoryFactoryMock.Setup(p => p.Repository<PortfolioProduct>().FirstOrDefaultAsync(It.IsAny<IQuery<PortfolioProduct>>(), default)).ReturnsAsync(portfolioProduct);

            var result = await _portfolioProductService.GetByRelationAsync(portfolioId, productId);

            result.Should().Be(portfolioProduct);

            _repositoryFactoryMock.Verify(p => p.Repository<PortfolioProduct>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<PortfolioProduct, bool>>>()), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<PortfolioProduct>().FirstOrDefaultAsync(It.IsAny<IQuery<PortfolioProduct>>(), default), Times.Once);
        }

        [Fact]
        public async void Should_Not_GetByRelationAsync_When_Relation_Doesnt_Exist()
        {
            long portfolioId = 1;
            long productId = 1;
            var portfolioProduct = PortfolioProductFixture.GeneratePortfolioProductFixture();

            _repositoryFactoryMock.Setup(p => p.Repository<PortfolioProduct>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<PortfolioProduct, bool>>>())).Returns(It.IsAny<IQuery<PortfolioProduct>>());
            _repositoryFactoryMock.Setup(p => p.Repository<PortfolioProduct>().FirstOrDefaultAsync(It.IsAny<IQuery<PortfolioProduct>>(), default));

            var action = () => _portfolioProductService.GetByRelationAsync(portfolioId, productId);

            await action.Should().ThrowAsync<ArgumentNullException>($"Nenhuma relação encontrada para ProductId: {productId} e PortfolioId: {portfolioId}");

            _repositoryFactoryMock.Verify(p => p.Repository<PortfolioProduct>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<PortfolioProduct, bool>>>()), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<PortfolioProduct>().FirstOrDefaultAsync(It.IsAny<IQuery<PortfolioProduct>>(), default), Times.Once);
        }

        [Fact]
        public async void Should_RelationAlreadyExists_Sucessfully()
        {
            long portfolioId = 1;
            long productId = 1;

            _repositoryFactoryMock.Setup(p => p.Repository<PortfolioProduct>().AnyAsync(It.IsAny<Expression<Func<PortfolioProduct, bool>>>(), default)).ReturnsAsync(true);

            var result = await _portfolioProductService.RelationAlreadyExistsAsync(portfolioId, productId);

            result.Should().BeTrue();

            _repositoryFactoryMock.Verify(p => p.Repository<PortfolioProduct>().AnyAsync(It.IsAny<Expression<Func<PortfolioProduct, bool>>>(), default), Times.Once);
        }

        [Fact]
        public async void Should_Not_RelationAlreadyExists_When_Relation_Doesnt_Exists()
        {
            long portfolioId = 1;
            long productId = 1;

            _repositoryFactoryMock.Setup(p => p.Repository<PortfolioProduct>().AnyAsync(It.IsAny<Expression<Func<PortfolioProduct, bool>>>(), default)).ReturnsAsync(false);

            var result = await _portfolioProductService.RelationAlreadyExistsAsync(portfolioId, productId);

            result.Should().BeFalse();

            _repositoryFactoryMock.Verify(p => p.Repository<PortfolioProduct>().AnyAsync(It.IsAny<Expression<Func<PortfolioProduct, bool>>>(), default), Times.Once);
        }
    }
}
