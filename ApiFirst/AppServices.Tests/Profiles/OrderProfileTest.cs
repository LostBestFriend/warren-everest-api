using AppModels.AppModels.Orders;
using AppModels.EnumModels;
using AutoMapper;
using DomainModels.Models;
using FluentAssertions;
using System;
using Xunit;

namespace AppServices.Tests.Profiles
{
    public class OrderProfileTest
    {
        private readonly IMapper _mapperUpdate;
        private readonly IMapper _mapperResponse;
        private readonly IMapper _mapperCreate;

        public OrderProfileTest()
        {
            _mapperUpdate = new MapperConfiguration(cfg =>
            cfg.CreateMap<UpdateOrder, Order>()).CreateMapper();
            _mapperResponse = new MapperConfiguration(cfg =>
            cfg.CreateMap<Order, OrderResponse>()).CreateMapper();
            _mapperCreate = new MapperConfiguration(cfg =>
            cfg.CreateMap<CreateOrder, Order>()).CreateMapper();
        }

        [Fact]
        public void Should_Map_UpdateOrder_Sucessfully()
        {
            var order = new Order(quotes: 1, unitPrice: 1, liquidateAt:
                new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                direction: DomainModels.Enums.OrderDirection.Buy,
                productId: 1, portfolioId: 1);

            var updateOrder = new UpdateOrder(quotes: 1, unitPrice: 1, liquidateAt:
                new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                direction: OrderDirection.Buy,
                productId: 1, portfolioId: 1);

            var result = _mapperUpdate.Map<Order>(updateOrder);

            result.Should().BeEquivalentTo(order);
        }

        [Fact]
        public void Should_Map_CreateOrder_Sucessfully()
        {
            var order = new Order(quotes: 1, unitPrice: 1, liquidateAt:
                new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                direction: DomainModels.Enums.OrderDirection.Buy,
                productId: 1, portfolioId: 1);
            var createOrder = new CreateOrder(quotes: 1, unitPrice: 1, liquidateAt:
                new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                direction: OrderDirection.Buy,
                productId: 1, portfolioId: 1);


            var result = _mapperCreate.Map<Order>(createOrder);

            result.Should().BeEquivalentTo(order);
        }

        [Fact]
        public void Should_Map_OrderResponse_Sucessfully()
        {
            var order = new Order(quotes: 1, unitPrice: 1, liquidateAt:
                new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                direction: DomainModels.Enums.OrderDirection.Buy,
                productId: 1, portfolioId: 1);
            var orderResponse = new OrderResponse(id: 1, quotes: 1,
                unitPrice: 1, liquidateAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                direction: OrderDirection.Buy,
                productId: 1, portfolioId: 1);

            var result = _mapperResponse.Map<OrderResponse>(order);

            result.Should().NotBeNull();
        }
    }
}
