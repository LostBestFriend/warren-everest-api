using AppModels.AppModels.Orders;
using AppModels.EnumModels;
using Bogus;
using System;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Order
{
    public class CreateOrderFixture
    {
        public static List<CreateOrder> GenerateCreateOrderFixture(int quantity)
        {
            return new Faker<CreateOrder>("en_US")
                .CustomInstantiator(p => new CreateOrder(
                    quotes: 1,
                    unitPrice: 1,
                    liquidatedAt: DateTime.Now.AddDays(-2),
                    direction: OrderDirection.Buy,
                    productId: 1,
                    portfolioId: 1))
                .Generate(quantity);
        }
        public static CreateOrder GenerateCreateOrderFixture()
        {
            return new Faker<CreateOrder>("en_US")
                .CustomInstantiator(p => new CreateOrder(
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
