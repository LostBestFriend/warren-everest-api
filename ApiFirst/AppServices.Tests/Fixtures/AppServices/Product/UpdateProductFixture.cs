using AppModels.AppModels.Products;
using AppModels.EnumModels;
using Bogus;
using System;
using System.Collections.Generic;

namespace ApiFirst.Tests.Fixtures.AppServices.Product
{
    public class UpdateProductFixture
    {
        public static List<UpdateProduct> GenerateUpdateProductFixture(int quantity)
        {
            return new Faker<UpdateProduct>("en_US")
                .CustomInstantiator(p => new UpdateProduct(
                    symbol: p.Image.ToString(),
                    unitPrice: p.Random.Decimal(0, 200000000),
                    issuanceAt: DateTime.Now.AddDays(-2),
                    expirationAt: DateTime.Now.AddDays(2),
                    type: p.PickRandom<ProductType>()))
                .Generate(quantity);
        }
        public static UpdateProduct GenerateUpdateProductFixture()
        {
            return new Faker<UpdateProduct>("en_US")
                .CustomInstantiator(p => new UpdateProduct(
                    symbol: p.Image.ToString(),
                    unitPrice: p.Random.Decimal(0, 200000000),
                    issuanceAt: DateTime.Now.AddDays(-2),
                    expirationAt: DateTime.Now.AddDays(2),
                    type: p.PickRandom<ProductType>()))
                .Generate();
        }
    }
}
