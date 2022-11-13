using AppModels.AppModels.Orders;
using AppModels.EnumModels;
using Bogus;
using System;
using System.Collections.Generic;

namespace ApiFirst.Tests.Fixtures.AppServices.Order
{
    public class UpdateOrderFixture
    {
        public static List<UpdateOrder> GenerateUpdateOrderFixture(int quantity)
        {
            return new Faker<UpdateOrder>("en_US")
                .CustomInstantiator(p => new UpdateOrder(
                    quotes: 1,
                    unitPrice: 1,
                    liquidatedAt: DateTime.Now.AddDays(-2),
                    direction: OrderDirection.Buy,
                    productId: 1,
                    portfolioId: 1))
                .Generate(quantity);
        }
        public static UpdateOrder GenerateUpdateOrderFixture()
        {
            return new Faker<UpdateOrder>("en_US")
                .CustomInstantiator(p => new UpdateOrder(
                    quotes: 1,
                    unitPrice: 1,
                    liquidatedAt: DateTime.Now.AddDays(-2),
                    direction: OrderDirection.Buy,
                    productId: 1,
                    portfolioId: 1))
                .Generate();
        }
    }
}
