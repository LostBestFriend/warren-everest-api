using ApiFirst.Tests.Fixtures.AppServices.Order;
using ApiFirst.Tests.Fixtures.DomainServices;
using AppModels.AppModels.Orders;
using AppServices.Profiles;
using AppServices.Services;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace ApiFirst.Tests.Services.AppServices
{
    public class OrderAppServiceTest
    {
        private readonly OrderAppService _orderAppService;
        private readonly Mock<IOrderService> _orderServiceMock;

        public OrderAppServiceTest()
        {
            IMapper _mapper = new MapperConfiguration(cfg => cfg.AddProfile<OrderProfile>()).CreateMapper();
            _orderServiceMock = new();
            _orderAppService = new OrderAppService(_mapper, _orderServiceMock.Object);
        }

        [Fact]
        public async void Should_CreateAsync_Sucessfully()
        {
            CreateOrder order = CreateOrderFixture.GenerateCreateOrderFixture();
            var id = 1;

            _orderServiceMock.Setup(p => p.CreateAsync(It.IsAny<Order>())).ReturnsAsync(id);

            var result = await _orderAppService.CreateAsync(order);

            result.Should().Be(id);

            _orderServiceMock.Verify(p => p.CreateAsync(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public async void Should_GetAll_Sucessfully()
        {
            var orders = OrderFixture.GenerateOrderFixture(3);
            var orderResponses = OrderResponseFixture.GenerateOrderResponseFixture(3);

            _orderServiceMock.Setup(p => p.GetAllAsync()).ReturnsAsync(orders);

            var result = await _orderAppService.GetAllAsync();

            result.Should().HaveCount(3);

            _orderServiceMock.Verify(p => p.GetAllAsync(), Times.Once);
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
        public async void Should_GetQuotesAvaliable_Sucessfully()
        {
            long portfolioId = 1;
            long productId = 1;
            int quantity = 1;

            _orderServiceMock.Setup(p => p.GetQuotesAvaliableAsync(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(quantity);

            var result = await _orderAppService.GetQuotesAvaliableAsync(portfolioId, productId);

            result.Should().Be(quantity);

            _orderServiceMock.Verify(p => p.GetQuotesAvaliableAsync(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async Task Should_GetExecutableOrdersAsync_Sucessfully()
        {
            var orders = OrderFixture.GenerateOrderFixture(3);
            var ordersResponse = OrderResponseFixture.GenerateOrderResponseFixture(3);

            _orderServiceMock.Setup(p => p.GetExecutableOrdersAsync()).ReturnsAsync(orders);

            var result = await _orderAppService.GetExecutableOrdersAsync();

            result.Should().HaveCount(3);

            _orderServiceMock.Verify(p => p.GetExecutableOrdersAsync(), Times.Once);
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
    }
}
