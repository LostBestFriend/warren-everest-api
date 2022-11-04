using AppModels.AppModels.Orders;
using AppModels.EnumModels;
using AppServices.Profiles;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Tests.Fixtures;
using FluentAssertions;
using Xunit;

namespace AppServices.Tests.Profiles
{
    public class OrderProfileTest
    {
        private readonly IMapper _mapper;

        public OrderProfileTest()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<OrderProfile>()).CreateMapper();
        }

        [Fact]
        public void Should_Map_UpdateOrder_Sucessfully()
        {
            var order = OrderFixture.GenerateOrderFixture();

            var updateOrder = new UpdateOrder(
                quotes: order.Quotes,
                unitPrice: order.NetValue / order.Quotes,
                liquidatedAt: order.LiquidatedAt,
                direction: OrderDirection.Buy,
                productId: order.ProductId,
                portfolioId: order.PortfolioId);

            var result = _mapper.Map<Order>(updateOrder);

            result.Should().BeEquivalentTo(order);
        }

        [Fact]
        public void Should_Map_CreateOrder_Sucessfully()
        {
            var order = OrderFixture.GenerateOrderFixture();

            var createOrder = new CreateOrder(
                quotes: order.Quotes,
                unitPrice: order.NetValue / order.Quotes,
                liquidatedAt: order.LiquidatedAt,
                direction: OrderDirection.Buy,
                productId: order.ProductId,
                portfolioId: order.PortfolioId);


            var result = _mapper.Map<Order>(createOrder);

            result.Should().BeEquivalentTo(order);
        }

        [Fact]
        public void Should_Map_OrderResponse_Sucessfully()
        {
            var order = OrderFixture.GenerateOrderFixture();

            var result = _mapper.Map<OrderResponse>(order);

            result.Should().NotBeNull();
        }
    }
}
