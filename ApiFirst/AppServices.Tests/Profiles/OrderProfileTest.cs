using AppModels.AppModels.Orders;
using AppModels.EnumModels;
using AutoMapper;
using DomainModels.Models;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace AppServices.Tests.Profiles
{
    public class OrderProfileTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IMapper> _mapperMock;

        public OrderProfileTest()
        {
            _mapperMock = new();
            _mapper = _mapperMock.Object;
        }

        [Fact]
        public void Should_Map_UpdateOrder_Sucessfully()
        {
            var order = new Order(quotes: 1, unitPrice: 1, liquidateAt:
                DateTime.Now.AddDays(-2),
                direction: DomainModels.Enums.OrderDirection.Buy,
                productId: 1, portfolioId: 1);

            var updateOrder = new UpdateOrder(quotes: 1, unitPrice: 1, liquidateAt:
                DateTime.Now.AddDays(-2),
                direction: OrderDirection.Buy,
                productId: 1, portfolioId: 1);

            _mapperMock.Setup(p => p.Map<Order>(It.IsAny<UpdateOrder>())).Returns(order);

            var result = _mapper.Map<Order>(updateOrder);

            result.Should().BeEquivalentTo(order);

            _mapperMock.Verify(p => p.Map<Order>(It.IsAny<UpdateOrder>()), Times.Once);
        }

        [Fact]
        public void Should_Map_CreateOrder_Sucessfully()
        {
            var order = new Order(quotes: 1, unitPrice: 1, liquidateAt:
                DateTime.Now.AddDays(-2),
                direction: DomainModels.Enums.OrderDirection.Buy,
                productId: 1, portfolioId: 1);
            var createOrder = new CreateOrder(quotes: 1, unitPrice: 1, liquidateAt:
                DateTime.Now.AddDays(-2),
                direction: OrderDirection.Buy,
                productId: 1, portfolioId: 1);

            _mapperMock.Setup(p => p.Map<Order>(It.IsAny<CreateOrder>())).Returns(order);

            var result = _mapper.Map<Order>(createOrder);

            _mapperMock.Verify(p => p.Map<Order>(It.IsAny<CreateOrder>()), Times.Once);

            result.Should().BeEquivalentTo(order);
        }

        [Fact]
        public void Should_Map_OrderResponse_Sucessfully()
        {
            var order = new Order(quotes: 1, unitPrice: 1, liquidateAt:
                DateTime.Now.AddDays(-2),
                direction: DomainModels.Enums.OrderDirection.Buy,
                productId: 1, portfolioId: 1);
            var orderResponse = new OrderResponse(id: 1, quotes: 1,
                unitPrice: 1, liquidateAt: DateTime.Now.AddDays(-2),
                direction: OrderDirection.Buy,
                productId: 1, portfolioId: 1);

            _mapperMock.Setup(p => p.Map<OrderResponse>(It.IsAny<Order>())).Returns(orderResponse);

            var result = _mapper.Map<OrderResponse>(order);

            result.Should().BeEquivalentTo(orderResponse);

            _mapperMock.Verify(p => p.Map<OrderResponse>(It.IsAny<Order>()), Times.Once);
        }
    }
}
