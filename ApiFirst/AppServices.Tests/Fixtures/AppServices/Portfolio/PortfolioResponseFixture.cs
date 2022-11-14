using ApiFirst.Tests.Fixtures.AppServices.Customer;
using ApiFirst.Tests.Fixtures.AppServices.Order;
using ApiFirst.Tests.Fixtures.AppServices.PortfolioProduct;
using AppModels.AppModels.Portfolios;
using Bogus;
using System.Collections.Generic;

namespace ApiFirst.Tests.Fixtures.AppServices.Portfolio
{
    public class PortfolioResponseFixture
    {
        public static List<PortfolioResponse> GeneratePortfolioResponseFixture(int quantity)
        {
            return new Faker<PortfolioResponse>("en_US")
                .CustomInstantiator(p => new PortfolioResponse(
                    id: p.Random.Long(0, 10),
                    name: p.Name.FirstName(),
                    description: p.Lorem.Text(),
                    totalBalance: p.Random.Decimal(0, 200000000),
                    accountBalance: p.Random.Decimal(0, 200000000),
                    customer: CustomerResponseFixture.GenerateCustomerResponseFixture(),
                    products: PortfolioProductResponseFixture.GeneratePortfolioProductResponseFixture(3),
                    orders: OrderResponseFixture.GenerateOrderResponseFixture(3)))
                .Generate(quantity);
        }
        public static PortfolioResponse GeneratePortfolioResponseFixture()
        {
            return new Faker<PortfolioResponse>("en_US")
                .CustomInstantiator(p => new PortfolioResponse(
                    id: p.Random.Long(0, 10),
                    name: p.Name.FirstName(),
                    description: p.Lorem.Text(),
                    totalBalance: p.Random.Decimal(0, 200000000),
                    accountBalance: p.Random.Decimal(0, 200000000),
                    customer: CustomerResponseFixture.GenerateCustomerResponseFixture(),
                    products: PortfolioProductResponseFixture.GeneratePortfolioProductResponseFixture(3),
                    orders: OrderResponseFixture.GenerateOrderResponseFixture(3)))
                .Generate();
        }
    }
}
