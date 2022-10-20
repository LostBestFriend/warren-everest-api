using AppModels.AppModels.Order;
using AppServices.Services;
using AppServices.Tests.Fixtures.Order;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using DomainServices.Tests.Fixtures;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace AppServices.Tests.Services
{
    public class OrderAppServiceTest
    {
        private readonly OrderAppService _orderAppService;
        private readonly Mock<IOrderService> _orderServiceMock;
        private readonly Mock<IMapper> _mapperMock;

        public OrderAppServiceTest()
        {
            _orderServiceMock = new();
            _mapperMock = new();
            _orderAppService = new OrderAppService(_mapperMock.Object, _orderServiceMock.Object);
        }

        [Fact]
        public async void Should_CreateAsync_Sucessfully()
        {
            CreateOrder order = CreateOrderFixture.GenerateCreateOrderFixture();

            _orderServiceMock.Setup(p => p.CreateAsync(It.IsAny<Order>())).ReturnsAsync(It.IsAny<long>());
            _mapperMock.Setup(p => p.Map<Order>(It.IsAny<CreateOrder>())).Returns(It.IsAny<Order>());

            var result = await _orderAppService.CreateAsync(order);

            result.Should().BeGreaterThanOrEqualTo(0);

            _orderServiceMock.Verify(p => p.CreateAsync(It.IsAny<Order>()), Times.Once);
            _mapperMock.Verify(p => p.Map<Order>(It.IsAny<CreateOrder>()), Times.Once);
        }

        [Fact]
        public void Should_GetAll_Sucessfully()
        {
            var orders = OrderFixture.GenerateOrderFixture(3);
            var orderResponses = OrderResponseFixture.GenerateOrderResponseFixture(3);

            _orderServiceMock.Setup(p => p.GetAll()).Returns(orders);
            _mapperMock.Setup(p => p.Map<IList<OrderResponse>>(It.IsAny<IList<Order>>())).Returns(orderResponses);

            _orderAppService.GetAll();

            _orderServiceMock.Verify(p => p.GetAll(), Times.Once);
            _mapperMock.Verify(p => p.Map<IList<OrderResponse>>(It.IsAny<IList<Order>>()), Times.Once);
        }

        [Fact]
        public void Should_GetByIdAsync_Sucessfully()
        {
            var orderId = 1;
            var orderResponse = OrderResponseFixture.GenerateOrderResponseFixture();

            _orderServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>()));
        }
    }
}
