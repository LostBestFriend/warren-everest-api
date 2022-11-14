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
                    quotes: p.Random.Int(0, 1000),
                    unitPrice: p.Random.Decimal(0, 200000000),
                    liquidatedAt: DateTime.Now.AddDays(-2),
                    direction: p.PickRandom<OrderDirection>(),
                    productId: p.Random.Long(0, 10),
                    portfolioId: p.Random.Long(0, 10)))
                .Generate(quantity);
        }
        public static UpdateOrder GenerateUpdateOrderFixture()
        {
            return new Faker<UpdateOrder>("en_US")
                .CustomInstantiator(p => new UpdateOrder(
                    quotes: p.Random.Int(0, 1000),
                    unitPrice: p.Random.Decimal(0, 200000000),
                    liquidatedAt: DateTime.Now.AddDays(-2),
                    direction: p.PickRandom<OrderDirection>(),
                    productId: p.Random.Long(0, 10),
                    portfolioId: p.Random.Long(0, 10)))
                .Generate();
        }
    }
}
