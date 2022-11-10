using AppModels.AppModels.Portfolios;
using AppServices.Validator.Portfolios;
using FluentAssertions;
using Xunit;

namespace AppServices.Tests.Validator
{
    public class PortfolioValidatorTest
    {

        public readonly CreatePortfolioValidator validatorCreate;
        public readonly UpdatePortfolioValidator validatorUpdate;

        public PortfolioValidatorTest()
        {
            validatorCreate = new CreatePortfolioValidator();
            validatorUpdate = new UpdatePortfolioValidator();
        }

        [Fact]
        public void Should_Validate_CreatePortfolio_Sucessfully()
        {
            var createPortfolio = new CreatePortfolio(name: "João",
                description: "aaa", customerId: 1);

            var result = validatorCreate.Validate(createPortfolio);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Validate_UpdatePortfolio_Sucessfully()
        {
            var updatePortfolio = new UpdatePortfolio(name: "João",
                description: "aaa", customerId: 1);

            var result = validatorUpdate.Validate(updatePortfolio);

            result.IsValid.Should().BeTrue();
        }
    }
}
