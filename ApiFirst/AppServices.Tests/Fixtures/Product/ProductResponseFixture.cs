using AppModels.AppModels.Product;
using Bogus;
using System;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Product
{
    public class ProductResponseFixture
    {
        public static List<ProductResponse> GenerateProductResponseFixture(int quantity)
        {
            return new Faker<ProductResponse>("en_US")
                .CustomInstantiator(p => new ProductResponse(id: 1, symbol: p.Image.ToString(),
                unitPrice: 1, issuanceAt: DateTime.Now.AddDays(-2),
                expirationAt: DateTime.Now.AddDays(2),
                type: AppModels.EnumModels.ProductEnum.FixedIncome))
                .Generate(quantity);
        }
        public static ProductResponse GenerateProductResponseFixture()
        {
            return new Faker<ProductResponse>("en_US")
                .CustomInstantiator(p => new ProductResponse(id: 1, symbol: p.Image.ToString(),
                unitPrice: 1, issuanceAt: DateTime.Now.AddDays(-2),
                expirationAt: DateTime.Now.AddDays(2),
                type: AppModels.EnumModels.ProductEnum.FixedIncome))
                .Generate();
        }
    }
}
