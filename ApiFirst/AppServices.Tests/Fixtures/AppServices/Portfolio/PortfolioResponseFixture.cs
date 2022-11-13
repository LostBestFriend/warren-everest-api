using AppModels.AppModels.Portfolios;
using AppServices.Tests.Fixtures.Customer;
using AppServices.Tests.Fixtures.Order;
using AppServices.Tests.Fixtures.PortfolioProduct;
using Bogus;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Portfolio
{
    public class PortfolioResponseFixture
    {
        public static List<PortfolioResponse> GeneratePortfolioResponseFixture(int quantity)
        {
            return new Faker<PortfolioResponse>("en_US")
                .CustomInstantiator(p => new PortfolioResponse(
                    id: 1, name: p.Name.FirstName(),
                    description: p.Lorem.Text(),
                    totalBalance: 1000,
                    accountBalance: 1000,
                    customer: CustomerResponseFixture.GenerateCustomerResponseFixture(),
                    products: PortfolioProductResponseFixture.GeneratePortfolioProductResponseFixture(3),
                    orders: OrderResponseFixture.GenerateOrderResponseFixture(3)))
                .Generate(quantity);
        }
        public static PortfolioResponse GeneratePortfolioResponseFixture()
        {
            return new Faker<PortfolioResponse>("en_US")
                .CustomInstantiator(p => new PortfolioResponse(
                    id: 1, name: p.Name.FirstName(),
                    description: p.Lorem.Text(),
                    totalBalance: 1000,
                    accountBalance: 1000,
                    customer: CustomerResponseFixture.GenerateCustomerResponseFixture(),
                    products: PortfolioProductResponseFixture.GeneratePortfolioProductResponseFixture(3),
                    orders: OrderResponseFixture.GenerateOrderResponseFixture(3)))
                .Generate();
        }
    }
}
