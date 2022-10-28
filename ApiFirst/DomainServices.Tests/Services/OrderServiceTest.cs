using DomainModels.Models;
using DomainServices.Services;
using DomainServices.Tests.Fixtures;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentAssertions;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace DomainServices.Tests.Services
{
    public class OrderServiceTest
    {
        readonly OrderService _orderService;
        readonly IUnitOfWork<WarrenContext> _unitOfWork;
        readonly IRepositoryFactory<WarrenContext> _repositoryFactory;
        readonly Mock<IRepositoryFactory<WarrenContext>> _repositoryFactoryMock;
        readonly Mock<IUnitOfWork<WarrenContext>> _unitOfWorkMock;

        public OrderServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork<WarrenContext>>();
            _unitOfWork = _unitOfWorkMock.Object;
            _repositoryFactoryMock = new Mock<IRepositoryFactory<WarrenContext>>();
            _repositoryFactory = _repositoryFactoryMock.Object;

            _orderService = new OrderService(_unitOfWork, _repositoryFactory);
        }

        [Fact]
        public async void Should_CreateAsync_Sucessfully()
        {
            var order = OrderFixture.GenerateOrderFixture();

            _unitOfWorkMock.Setup(p => p.Repository<Order>().AddAsync(It.IsAny<Order>(), default));
            _unitOfWorkMock.Setup(p => p.SaveChangesAsync(true, false, default));

            var result = await _orderService.CreateAsync(order);

            result.Should().BeGreaterThanOrEqualTo(0);

            _unitOfWorkMock.Verify(p => p.Repository<Order>().AddAsync(It.IsAny<Order>(), default), Times.Once);
            _unitOfWorkMock.Verify(p => p.SaveChangesAsync(true, false, default), Times.Once);
        }

        [Fact]
        public async void Should_GetAll_Sucessfully()
        {
            List<Order> orderList = OrderFixture.GenerateOrderFixture(2);

            _repositoryFactoryMock.Setup(p => p.Repository<Order>().MultipleResultQuery().Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>())).Returns(It.IsAny<IMultipleResultQuery<Order>>());
            _repositoryFactoryMock.Setup(p => p.Repository<Order>().SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default)).ReturnsAsync(orderList);

            var customers = await _orderService.GetAllAsync();

            customers.Should().HaveCountGreaterThanOrEqualTo(0);

            _repositoryFactoryMock.Verify(p => p.Repository<Order>().SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default), Times.Once);
        }

        [Fact]
        public async void Should_GetByIdAsync_Sucessfully()
        {
            var order = OrderFixture.GenerateOrderFixture();
            long id = 1;

            _repositoryFactoryMock.Setup(p => p.Repository<Order>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>()).Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>())).Returns(It.IsAny<IQuery<Order>>());
            _repositoryFactoryMock.Setup(p => p.Repository<Order>().FirstOrDefaultAsync(It.IsAny<IQuery<Order>>(), default)).ReturnsAsync(order);

            var result = await _orderService.GetByIdAsync(id);
            result.Should().NotBeNull();

            _repositoryFactoryMock.Verify(p => p.Repository<Order>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>()).Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<Order>().FirstOrDefaultAsync(It.IsAny<IQuery<Order>>(), default), Times.Once);
        }

        [Fact]
        public async void Should_GetExecutableOrders_Sucessfully()
        {
            var orders = OrderFixture.GenerateOrderFixture(3);

            _repositoryFactoryMock.Setup(p => p.Repository<Order>().MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())).Returns(It.IsAny<IQuery<Order>>());
            _repositoryFactoryMock.Setup(p => p.Repository<Order>().SearchAsync(It.IsAny<IQuery<Order>>(), default)).ReturnsAsync(orders);

            var result = await _orderService.GetExecutableOrdersAsync();
            result.Should().HaveCountGreaterThanOrEqualTo(0);

            _repositoryFactoryMock.Verify(p => p.Repository<Order>().MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>()), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<Order>().SearchAsync(It.IsAny<IQuery<Order>>(), default), Times.Once);
        }

        [Fact]
        public void Should_Update_Sucessfully()
        {
            var order = OrderFixture.GenerateOrderFixture();
            _unitOfWorkMock.Setup(P => P.Repository<Order>().Update(It.IsAny<Order>()));
            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            _orderService.Update(order);

            _unitOfWorkMock.Verify(P => P.Repository<Order>().Update(It.IsAny<Order>()), Times.Once);
            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Once);
        }

        [Fact]
        public async void Should_GetQuotesAvaliable_Sucessfully()
        {
            long portfolioId = 1;
            long productId = 1;
            var orders = OrderFixture.GenerateOrderFixture(3);

            _repositoryFactoryMock.Setup(p => p.Repository<Order>().MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())).Returns(It.IsAny<IQuery<Order>>());
            _repositoryFactoryMock.Setup(p => p.Repository<Order>().SearchAsync(It.IsAny<IQuery<Order>>(), default)).ReturnsAsync(orders);
            _repositoryFactoryMock.Setup(p => p.Repository<Order>().Any(null)).Returns(true);

            var result = await _orderService.GetQuotesAvaliableAsync(portfolioId, productId);
            result.Should().BeGreaterThanOrEqualTo(0);

            _repositoryFactoryMock.Verify(p => p.Repository<Order>().MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>()), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<Order>().SearchAsync(It.IsAny<IQuery<Order>>(), default), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<Order>().Any(null), Times.Never);
        }

        [Fact]
        public async void Should_Not_GetQuotesAvaliable_When_Avaliable_Quotes_Doesnt_Exist()
        {
            long portfolioId = 1;
            long productId = 1;
            var order = OrderFixture.GenerateOrderFixture(3);
            List<Order> list = new();

            _repositoryFactoryMock.Setup(p => p.Repository<Order>().MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())).Returns(It.IsAny<IQuery<Order>>());
            _repositoryFactoryMock.Setup(p => p.Repository<Order>().SearchAsync(It.IsAny<IQuery<Order>>(), default)).ReturnsAsync(list);

            try
            {
                var result = await _orderService.GetQuotesAvaliableAsync(portfolioId, productId);
                result.Should().BeGreaterThanOrEqualTo(0);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }

            _repositoryFactoryMock.Verify(p => p.Repository<Order>().MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>()), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<Order>().SearchAsync(It.IsAny<IQuery<Order>>(), default), Times.Once);
        }
    }
}
