using Bogus;
using DomainModels.Enums;
using DomainModels.Models;
using System;
using System.Collections.Generic;

namespace ApiFirst.Tests.Fixtures.DomainServices
{
    public class ProductFixture
    {
        public static List<Product> GenerateProductFixture(int quantity)
        {
            return new Faker<Product>("en_US")
                .CustomInstantiator(p => new Product(
                    symbol: p.Image.ToString(),
                    unitPrice: 1,
                    issuanceAt: DateTime.Now.AddDays(-2),
                    expirationAt: DateTime.Now.AddDays(2),
                    type: ProductType.FixedIncome))
                .Generate(quantity);
        }
        public static Product GenerateProductFixture()
        {
            return new Faker<Product>("en_US")
                .CustomInstantiator(p => new Product(
                    symbol: p.Image.ToString(),
                    unitPrice: 1,
                    issuanceAt: DateTime.Now.AddDays(-2),
                    expirationAt: DateTime.Now.AddDays(2),
                    type: ProductType.FixedIncome))
                .Generate();
        }
    }
}
