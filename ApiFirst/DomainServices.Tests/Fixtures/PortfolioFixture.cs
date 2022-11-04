using Bogus;
using DomainModels.Models;
using System.Collections.Generic;

namespace DomainServices.Tests.Fixtures
{
    public class PortfolioFixture
    {
        public static List<Portfolio> GeneratePortfolioFixture(int quantity)
        {
            return new Faker<Portfolio>("en_US")
                .CustomInstantiator(p => new Portfolio(
                    name: p.Name.FirstName(),
                    description: p.Lorem.Text(),
                    totalBalance: p.PickRandom(100, 200, 500, 1000),
                    customerId: 1))
                .Generate(quantity);
        }
        public static Portfolio GeneratePortfolioFixture()
        {
            return new Faker<Portfolio>("en_US")
                .CustomInstantiator(p => new Portfolio(
                    name: p.Name.FirstName(),
                    description: p.Lorem.Text(),
                    totalBalance: p.PickRandom(100, 200, 500, 1000),
                    customerId: 1))
                .Generate();
        }
    }
}
