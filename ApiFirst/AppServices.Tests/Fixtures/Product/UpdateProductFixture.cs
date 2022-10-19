using AppModels.AppModels.Product;
using Bogus;
using System;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Product
{
    public class UpdateProductFixture
    {
        public static List<UpdateProduct> GenerateUpdateProductFixture(int quantity)
        {
            return new Faker<UpdateProduct>("en_US")
                .CustomInstantiator(p => new UpdateProduct(symbol: p.Image.ToString(),
                unitPrice: 1, issuanceAt: DateTime.Now.AddDays(-2),
                expirationAt: DateTime.Now.AddDays(2),
                type: AppModels.EnumModels.ProductEnum.FixedIncome))
                .Generate(quantity);
        }
        public static UpdateProduct GenerateUpdateProductFixture()
        {
            return new Faker<UpdateProduct>("en_US")
                .CustomInstantiator(p => new UpdateProduct(symbol: p.Image.ToString(),
                unitPrice: 1, issuanceAt: DateTime.Now.AddDays(-2),
                expirationAt: DateTime.Now.AddDays(2),
                type: AppModels.EnumModels.ProductEnum.FixedIncome))
                .Generate();
        }
    }
}
