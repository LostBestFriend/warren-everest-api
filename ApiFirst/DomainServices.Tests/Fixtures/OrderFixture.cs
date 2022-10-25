using Bogus;
using DomainModels.Enums;
using DomainModels.Models;
using System;
using System.Collections.Generic;

namespace DomainServices.Tests.Fixtures
{
    public class OrderFixture
    {
        public static List<Order> GenerateOrderFixture(int quantity)
        {
            return new Faker<Order>("en_US")
                .CustomInstantiator(p => new Order(quotes: 1,
                unitPrice: 1, liquidateAt: DateTime.Now.AddDays(-2),
                direction: OrderDirection.Buy, productId: 1, portfolioId: 1))
                .Generate(quantity);
        }
        public static Order GenerateOrderFixture()
        {
            return new Faker<Order>("en_US")
                .CustomInstantiator(p => new Order(quotes: 1,
                unitPrice: 1, liquidateAt: DateTime.Now.AddDays(-2),
                direction: OrderDirection.Buy, productId: 1, portfolioId: 1))
                .Generate();
        }
    }
}
