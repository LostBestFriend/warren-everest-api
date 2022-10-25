using AppModels.AppModels.Orders;
using AppModels.EnumModels;
using Bogus;
using System;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Order
{
    public class OrderResponseFixture
    {
        public static List<OrderResponse> GenerateOrderResponseFixture(int quantity)
        {
            return new Faker<OrderResponse>("en_US")
                .CustomInstantiator(p => new OrderResponse(id: 1, quotes: 1, unitPrice: 1,
                liquidateAt: DateTime.Now.AddDays(-2), direction: OrderDirection.Buy,
                productId: 1, portfolioId: 1))
                .Generate(quantity);
        }
        public static OrderResponse GenerateOrderResponseFixture()
        {
            return new Faker<OrderResponse>("en_US")
                .CustomInstantiator(p => new OrderResponse(id: 1, quotes: 1, unitPrice: 1,
                liquidateAt: DateTime.Now.AddDays(-2), direction: OrderDirection.Buy,
                productId: 1, portfolioId: 1))
                .Generate();
        }
    }
}
