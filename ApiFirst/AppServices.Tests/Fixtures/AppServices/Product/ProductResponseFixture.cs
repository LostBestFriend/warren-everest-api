using AppModels.AppModels.Products;
using AppModels.EnumModels;
using Bogus;
using System;
using System.Collections.Generic;

namespace ApiFirst.Tests.Fixtures.AppServices.Product
{
    public class ProductResponseFixture
    {
        public static List<ProductResponse> GenerateProductResponseFixture(int quantity)
        {
            return new Faker<ProductResponse>("en_US")
                .CustomInstantiator(p => new ProductResponse(
                    id: p.Random.Long(0, 10),
                    symbol: p.Image.ToString(),
                    unitPrice: p.Random.Decimal(0, 200000000),
                    issuanceAt: DateTime.Now.AddDays(-2),
                    expirationAt: DateTime.Now.AddDays(2),
                    type: p.PickRandom<ProductType>()))
                .Generate(quantity);
        }
        public static ProductResponse GenerateProductResponseFixture()
        {
            return new Faker<ProductResponse>("en_US")
                .CustomInstantiator(p => new ProductResponse(
                    id: p.Random.Long(0, 10),
                    symbol: p.Image.ToString(),
                    unitPrice: p.Random.Decimal(0, 200000000),
                    issuanceAt: DateTime.Now.AddDays(-2),
                    expirationAt: DateTime.Now.AddDays(2),
                    type: p.PickRandom<ProductType>()))
                .Generate();
        }
    }
}
