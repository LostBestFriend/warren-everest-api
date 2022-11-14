using Bogus;
using DomainModels.Enums;
using DomainModels.Models;
using System;
using System.Collections.Generic;

namespace ApiFirst.Tests.Fixtures.DomainServices
{
    public class OrderFixture
    {
        public static List<Order> GenerateOrderFixture(int quantity)
        {
            return new Faker<Order>("en_US")
                .CustomInstantiator(p => new Order(
                    quotes: p.Random.Int(0, 1000),
                    unitPrice: p.Random.Decimal(0, 200000000),
                    liquidatedAt: DateTime.Now.AddDays(-2),
                    direction: p.PickRandom<OrderDirection>(),
                    productId: p.Random.Long(0, 10),
                    portfolioId: p.Random.Long(0, 10)))
                .Generate(quantity);
        }
        public static Order GenerateOrderFixture()
        {
            return new Faker<Order>("en_US")
                .CustomInstantiator(p => new Order(
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
