using AppModels.AppModels.Order;
using AppServices.Services;
using AppServices.Tests.Fixtures.Order;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using DomainServices.Tests.Fixtures;
using FluentAssertions;
using Moq;
using Xunit;

namespace AppServices.Tests.Services
{
    public class OrderAppServiceTest
    {
        private readonly OrderAppService _orderAppService;
        private readonly Mock<IOrderService> _orderServiceMock;

        public OrderAppServiceTest()
        {
            _orderServiceMock = new();
            Mock<IMapper> _mapperMock = new();
            _orderAppService = new OrderAppService(_mapperMock.Object, _orderServiceMock.Object);
        }

        [Fact]
        public async void Should_CreateAsync_Sucessfully()
        {
            CreateOrder order = CreateOrderFixture.GenerateCreateOrderFixture();

            _orderServiceMock.Setup(p => p.CreateAsync(It.IsAny<Order>())).ReturnsAsync(It.IsAny<long>());

            var result = await _orderAppService.CreateAsync(order);

            result.Should().BeGreaterThanOrEqualTo(0);

            _orderServiceMock.Verify(p => p.CreateAsync(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public void Should_GetAll_Sucessfully()
        {
            var orders = OrderFixture.GenerateOrderFixture(3);
            var orderResponses = OrderResponseFixture.GenerateOrderResponseFixture(3);

            _orderServiceMock.Setup(p => p.GetAll()).Returns(orders);

            _orderAppService.GetAll();

            _orderServiceMock.Verify(p => p.GetAll(), Times.Once);
        }

        [Fact]
        public async void Should_GetByIdAsync_Sucessfully()
        {
            var orderId = 1;
            var orderResponse = OrderResponseFixture.GenerateOrderResponseFixture();
            var order = OrderFixture.GenerateOrderFixture();

            _orderServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(order);

            var result = await _orderAppService.GetByIdAsync(orderId);

            result.Should().NotBeNull();

            _orderServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public void Should_GetQuotesAvaliable_Sucessfully()
        {
            long portfolioId = 1;
            long productId = 1;
            int quantity = 1;

            _orderServiceMock.Setup(p => p.GetQuotesAvaliable(It.IsAny<long>(), It.IsAny<long>())).Returns(quantity);

            var result = _orderAppService.GetQuotesAvaliable(portfolioId, productId);
            result.Should().BeGreaterThanOrEqualTo(0);

            _orderServiceMock.Verify(p => p.GetQuotesAvaliable(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public void Should_GetExecutableOrders_Sucessfully()
        {
            var orders = OrderFixture.GenerateOrderFixture(3);
            var ordersResponse = OrderResponseFixture.GenerateOrderResponseFixture(3);

            _orderServiceMock.Setup(p => p.GetExecutableOrders()).Returns(orders);

            var result = _orderAppService.GetExecutableOrders();
            result.Should().NotBeNull();

            _orderServiceMock.Verify(p => p.GetExecutableOrders(), Times.Once);
        }

        [Fact]
        public void Should_Update_Sucessfully()
        {
            var updateOrder = UpdateOrderFixture.GenerateUpdateOrderFixture();
            long id = 1;
            var order = OrderFixture.GenerateOrderFixture();

            _orderServiceMock.Setup(p => p.Update(It.IsAny<Order>()));

            _orderAppService.Update(id, updateOrder);

            _orderServiceMock.Verify(p => p.Update(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public void Should_Delete_Sucessfully()
        {
            long id = 1;

            _orderServiceMock.Setup(p => p.Delete(It.IsAny<long>()));

            _orderAppService.Delete(id);

            _orderServiceMock.Verify(p => p.Delete(It.IsAny<long>()), Times.Once);
        }
    }
}
