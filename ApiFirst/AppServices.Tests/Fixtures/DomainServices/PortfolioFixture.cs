using Bogus;
using DomainModels.Models;
using System.Collections.Generic;

namespace ApiFirst.Tests.Fixtures.DomainServices
{
    public class PortfolioFixture
    {
        public static List<Portfolio> GeneratePortfolioFixture(int quantity)
        {
            return new Faker<Portfolio>("en_US")
                .CustomInstantiator(p => new Portfolio(
                    name: p.Name.FirstName(),
                    description: p.Lorem.Text(),
                    totalBalance: p.Random.Decimal(0, 200000000),
                    customerId: p.Random.Long(0, 10)))
                .Generate(quantity);
        }
        public static Portfolio GeneratePortfolioFixture()
        {
            return new Faker<Portfolio>("en_US")
                .CustomInstantiator(p => new Portfolio(
                    name: p.Name.FirstName(),
                    description: p.Lorem.Text(),
                    totalBalance: p.Random.Decimal(0, 200000000),
                    customerId: p.Random.Long(0, 10)))
                .Generate();
        }
    }
}
