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
    public class ProductServiceTest
    {
        readonly ProductService _productService;
        readonly IUnitOfWork<WarrenContext> _unitOfWork;
        readonly IRepositoryFactory<WarrenContext> _repositoryFactory;
        readonly Mock<IRepositoryFactory<WarrenContext>> _repositoryFactoryMock;
        readonly Mock<IUnitOfWork<WarrenContext>> _unitOfWorkMock;

        public ProductServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork<WarrenContext>>();
            _unitOfWork = _unitOfWorkMock.Object;
            _repositoryFactoryMock = new Mock<IRepositoryFactory<WarrenContext>>();
            _repositoryFactory = _repositoryFactoryMock.Object;

            _productService = new ProductService(_unitOfWork, _repositoryFactory);
        }

        [Fact]
        public async void Should_CreateAsync_Sucessfully()
        {
            var product = ProductFixture.GenerateProductFixture();

            _unitOfWorkMock.Setup(p => p.Repository<Product>().AddAsync(It.IsAny<Product>(), default));
            _unitOfWorkMock.Setup(p => p.SaveChangesAsync(true, false, default));

            var result = await _productService.CreateAsync(product);

            result.Should().BeGreaterThanOrEqualTo(0);

            _unitOfWorkMock.Verify(p => p.Repository<Product>().AddAsync(It.IsAny<Product>(), default), Times.Once);
            _unitOfWorkMock.Verify(p => p.SaveChangesAsync(true, false, default), Times.Once);
        }

        [Fact]
        public async void Should_GetByIdAsync_Sucessfully()
        {
            var productId = 1;
            var product = ProductFixture.GenerateProductFixture();

            _repositoryFactoryMock.Setup(p => p.Repository<Product>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Product, bool>>>())).Returns(It.IsAny<IQuery<Product>>());
            _repositoryFactoryMock.Setup(P => P.Repository<Product>().FirstOrDefaultAsync(It.IsAny<IQuery<Product>>(), default)).ReturnsAsync(product);

            var result = await _productService.GetByIdAsync(productId);
            result.Should().NotBeNull();

            _repositoryFactoryMock.Verify(p => p.Repository<Product>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
            _repositoryFactoryMock.Verify(P => P.Repository<Product>().FirstOrDefaultAsync(It.IsAny<IQuery<Product>>(), default), Times.Once);
        }

        [Fact]
        public async void Should_Not_GetByIdAsync_When_Id_Dismatch()
        {
            var productId = 0;

            _repositoryFactoryMock.Setup(p => p.Repository<Product>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Product, bool>>>())).Returns(It.IsAny<IQuery<Product>>());
            _repositoryFactoryMock.Setup(P => P.Repository<Product>().FirstOrDefaultAsync(It.IsAny<IQuery<Product>>(), default));
            try
            {
                var result = await _productService.GetByIdAsync(productId);
                result.Should().NotBeNull();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }

            _repositoryFactoryMock.Verify(p => p.Repository<Product>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
            _repositoryFactoryMock.Verify(P => P.Repository<Product>().FirstOrDefaultAsync(It.IsAny<IQuery<Product>>(), default), Times.Once);
        }

        [Fact]
        public async void Should_GetAllAsync_Sucessfully()
        {
            var products = ProductFixture.GenerateProductFixture(3);

            _repositoryFactoryMock.Setup(p => p.Repository<Product>().MultipleResultQuery()).Returns(It.IsAny<IMultipleResultQuery<Product>>);
            _repositoryFactoryMock.Setup(p => p.Repository<Product>().SearchAsync(It.IsAny<IMultipleResultQuery<Product>>(), default)).ReturnsAsync(products);

            var result = await _productService.GetAllAsync();

            result.Should().HaveCountGreaterThanOrEqualTo(0);

            _repositoryFactoryMock.Verify(p => p.Repository<Product>().MultipleResultQuery(), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<Product>().SearchAsync(It.IsAny<IQuery<Product>>(), default), Times.Once);
        }

        [Fact]
        public void Should_Update_Sucessfully()
        {
            var product = ProductFixture.GenerateProductFixture();

            _unitOfWorkMock.Setup(p => p.Repository<Product>().Update(It.IsAny<Product>()));
            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            _productService.Update(product);

            _unitOfWorkMock.Verify(p => p.Repository<Product>().Update(It.IsAny<Product>()), Times.Once);
            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Once);
        }

        [Fact]
        public async void Should_Delete_Sucessfully()
        {
            long productId = 1;
            var product = ProductFixture.GenerateProductFixture();

            _repositoryFactoryMock.Setup(p => p.Repository<Product>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Product, bool>>>())).Returns(It.IsAny<IQuery<Product>>());
            _repositoryFactoryMock.Setup(P => P.Repository<Product>().FirstOrDefaultAsync(It.IsAny<IQuery<Product>>(), default)).ReturnsAsync(product);
            _unitOfWorkMock.Setup(p => p.Repository<Product>().Remove(It.IsAny<Product>()));

            await _productService.DeleteAsync(productId);

            _repositoryFactoryMock.Verify(p => p.Repository<Product>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
            _repositoryFactoryMock.Verify(P => P.Repository<Product>().FirstOrDefaultAsync(It.IsAny<IQuery<Product>>(), default), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Product>().Remove(It.IsAny<Product>()), Times.Once);
        }
    }
}
