using AppModels.AppModels.Product;
using Bogus;
using System;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Product
{
    public class CreateProductFixture
    {
        public static List<CreateProduct> GenerateCreateProductFixture(int quantity)
        {
            return new Faker<CreateProduct>("en_US")
                .CustomInstantiator(p => new CreateProduct(symbol: p.Image.ToString(),
                unitPrice: 1, issuanceAt: DateTime.Now.AddDays(-2),
                expirationAt: DateTime.Now.AddDays(2),
                type: AppModels.EnumModels.ProductEnum.FixedIncome))
                .Generate(quantity);
        }
        public static CreateProduct GenerateCreateProductFixture()
        {
            return new Faker<CreateProduct>("en_US")
                .CustomInstantiator(p => new CreateProduct(symbol: p.Image.ToString(),
                unitPrice: 1, issuanceAt: DateTime.Now.AddDays(-2),
                expirationAt: DateTime.Now.AddDays(2),
                type: AppModels.EnumModels.ProductEnum.FixedIncome))
                .Generate();
        }
    }
}
