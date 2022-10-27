using AppModels.AppModels.Orders;
using AppModels.EnumModels;
using AppServices.Profiles;
using AutoMapper;
using DomainModels.Models;
using FluentAssertions;
using System;
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
            var order = new Order(quotes: 1, unitPrice: 1, liquidateAt:
                new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                direction: DomainModels.Enums.OrderDirection.Buy,
                productId: 1, portfolioId: 1);

            var updateOrder = new UpdateOrder(quotes: 1, unitPrice: 1, liquidateAt:
                new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                direction: OrderDirection.Buy,
                productId: 1, portfolioId: 1);

            var result = _mapper.Map<Order>(updateOrder);

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


            var result = _mapper.Map<Order>(createOrder);

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

            var result = _mapper.Map<OrderResponse>(order);

            result.Should().NotBeNull();
        }
    }
}
