using AppModels.AppModels.Portfolios;
using Bogus;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Portfolio
{
    public class CreatePortfolioFixture
    {
        public static List<CreatePortfolio> GenerateCreatePortfolioFixture(int quantity)
        {
            return new Faker<CreatePortfolio>("en_US")
                .CustomInstantiator(p => new CreatePortfolio(
                    name: p.Name.FirstName(),
                    description: p.Lorem.Text(),
                    customerId: 1))
                .Generate(quantity);
        }
        public static CreatePortfolio GenerateCreatePortfolioFixture()
        {
            return new Faker<CreatePortfolio>("en_US")
                .CustomInstantiator(p => new CreatePortfolio(
                    name: p.Name.FirstName(),
                    description: p.Lorem.Text(),
                    customerId: 1))
                .Generate();
        }
    }
}
