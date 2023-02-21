﻿using AppModels.AppModels.Orders;
using AppModels.EnumModels;
using Bogus;
using System;
using System.Collections.Generic;

namespace ApiFirst.Tests.Fixtures.AppServices.Order
{
    public class OrderResponseFixture
    {
        public static List<OrderResponse> GenerateOrderResponseFixture(int quantity)
        {
            return new Faker<OrderResponse>("en_US")
                .CustomInstantiator(p => new OrderResponse(
                    id: p.Random.Long(0, 10),
                    quotes: p.Random.Int(0, 1000),
                    unitPrice: p.Random.Decimal(0, 200000000),
                    liquidatedAt: DateTime.Now.AddDays(-2),
                    direction: p.PickRandom<OrderDirection>(),
                    productId: p.Random.Long(1, 10),
                    portfolioId: p.Random.Long(1, 10)))
                .Generate(quantity);
        }
        public static OrderResponse GenerateOrderResponseFixture()
        {
            return new Faker<OrderResponse>("en_US")
                .CustomInstantiator(p => new OrderResponse(
                    id: p.Random.Long(0, 10),
                    quotes: p.Random.Int(0, 1000),
                    unitPrice: p.Random.Decimal(0, 200000000),
                    liquidatedAt: DateTime.Now.AddDays(-2),
                    direction: p.PickRandom<OrderDirection>(),
                    productId: p.Random.Long(1, 10),
                    portfolioId: p.Random.Long(1, 10)))
                .Generate();
        }
    }
}
