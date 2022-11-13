using AppModels.AppModels.Products;
using AppModels.EnumModels;
using Bogus;
using System;
using System.Collections.Generic;

namespace ApiFirst.Tests.Fixtures.AppServices.Product
{
    public class CreateProductFixture
    {
        public static List<CreateProduct> GenerateCreateProductFixture(int quantity)
        {
            return new Faker<CreateProduct>("en_US")
                .CustomInstantiator(p => new CreateProduct(
                    symbol: p.Image.ToString(),
                    unitPrice: 1,
                    issuanceAt: DateTime.Now.AddDays(-2),
                    expirationAt: DateTime.Now.AddDays(2),
                    type: ProductType.FixedIncome))
                .Generate(quantity);
        }
        public static CreateProduct GenerateCreateProductFixture()
        {
            return new Faker<CreateProduct>("en_US")
                .CustomInstantiator(p => new CreateProduct(
                    symbol: p.Image.ToString(),
                    unitPrice: 1,
                    issuanceAt: DateTime.Now.AddDays(-2),
                    expirationAt: DateTime.Now.AddDays(2),
                    type: ProductType.FixedIncome))
                .Generate();
        }
    }
}
