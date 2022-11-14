using AppModels.AppModels.Portfolios;
using Bogus;
using System.Collections.Generic;

namespace ApiFirst.Tests.Fixtures.AppServices.Portfolio
{
    public class CreatePortfolioFixture
    {
        public static List<CreatePortfolio> GenerateCreatePortfolioFixture(int quantity)
        {
            return new Faker<CreatePortfolio>("en_US")
                .CustomInstantiator(p => new CreatePortfolio(
                    name: p.Name.FirstName(),
                    description: p.Lorem.Text(),
                    customerId: p.Random.Long(0, 10)))
                .Generate(quantity);
        }
        public static CreatePortfolio GenerateCreatePortfolioFixture()
        {
            return new Faker<CreatePortfolio>("en_US")
                .CustomInstantiator(p => new CreatePortfolio(
                    name: p.Name.FirstName(),
                    description: p.Lorem.Text(),
                    customerId: p.Random.Long(0, 10)))
                .Generate();
        }
    }
}
