using AppModels.AppModels.Portfolios;
using Bogus;
using System.Collections.Generic;

namespace ApiFirst.Tests.Fixtures.AppServices.Portfolio
{
    public class UpdatePortfolioFixture
    {
        public static List<UpdatePortfolio> GenerateUpdatePortfolioFixture(int quantity)
        {
            return new Faker<UpdatePortfolio>("en_US")
                .CustomInstantiator(p => new UpdatePortfolio(
                    name: p.Name.FirstName(),
                    description: p.Lorem.Text(),
                    customerId: 1))
                .Generate(quantity);
        }
        public static UpdatePortfolio GenerateUpdatePortfolioFixture()
        {
            return new Faker<UpdatePortfolio>("en_US")
                .CustomInstantiator(p => new UpdatePortfolio(
                    name: p.Name.FirstName(),
                    description: p.Lorem.Text(),
                    customerId: 1))
                .Generate();
        }
    }
}
