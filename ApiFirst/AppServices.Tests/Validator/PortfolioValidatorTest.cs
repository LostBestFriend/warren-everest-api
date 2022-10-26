using AppModels.AppModels.Portfolios;
using AppServices.Validator.Portfolios;
using FluentAssertions;
using Xunit;

namespace AppServices.Tests.Validator
{
    public class PortfolioValidatorTest
    {
        [Fact]
        public void Should_Validate_CreatePortfolio_Sucessfully()
        {
            var createPortfolio = new CreatePortfolio(name: "João",
                description: "aaa", customerId: 1);
            var validator = new CreatePortfolioValidator();

            var result = validator.Validate(createPortfolio);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Validate_UpdatePortfolio_Sucessfully()
        {
            var updatePortfolio = new UpdatePortfolio(name: "João",
                description: "aaa", customerId: 1);
            var validator = new UpdatePortfolioValidator();
            var result = validator.Validate(updatePortfolio);

            result.IsValid.Should().BeTrue();
        }
    }
}
