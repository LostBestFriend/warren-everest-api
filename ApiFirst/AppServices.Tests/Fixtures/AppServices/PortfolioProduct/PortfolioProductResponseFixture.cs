using ApiFirst.Tests.Fixtures.AppServices.Product;
using AppModels.AppModels.PortfolioProducts;
using Bogus;
using System.Collections.Generic;

namespace ApiFirst.Tests.Fixtures.AppServices.PortfolioProduct
{
    public class PortfolioProductResponseFixture
    {
        public static List<PortfolioProductResponse> GeneratePortfolioProductResponseFixture(int quantity)
        {
            return new Faker<PortfolioProductResponse>("en_US")
                .CustomInstantiator(p => new PortfolioProductResponse(
                    product: ProductResponseFixture.GenerateProductResponseFixture()))
                .Generate(quantity);
        }
        public static PortfolioProductResponse GeneratePortfolioProductResponseFixture()
        {
            return new Faker<PortfolioProductResponse>("en_US")
                .CustomInstantiator(p => new PortfolioProductResponse(
                    product: ProductResponseFixture.GenerateProductResponseFixture()))
                .Generate();
        }
    }
}
