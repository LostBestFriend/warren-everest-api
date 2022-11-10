using AppModels.AppModels.Portfolios;
using AppServices.Validator.Portfolios;
using FluentAssertions;
using FluentValidation.TestHelper;
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
        public void Should_Not_Validate_CreatePortfolio_When_Name_Is_Empty()
        {
            var createPortfolio = new CreatePortfolio(name: "",
                description: "aaa", customerId: 1);

            var result = validatorCreate.TestValidate(createPortfolio);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Not_Validate_CreatePortfolio_When_Description_Is_Empty()
        {
            var createPortfolio = new CreatePortfolio(name: "aaa",
                description: "", customerId: 1);

            var result = validatorCreate.TestValidate(createPortfolio);

            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Should_Validate_UpdatePortfolio_Sucessfully()
        {
            var updatePortfolio = new UpdatePortfolio(name: "João",
                description: "aaa", customerId: 1);

            var result = validatorUpdate.Validate(updatePortfolio);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Not_Validate_UpdatePortfolio_When_Name_Is_Empty()
        {
            var updatePortfolio = new UpdatePortfolio(name: "",
                description: "aaa", customerId: 1);

            var result = validatorUpdate.TestValidate(updatePortfolio);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Not_Validate_UpdatePortfolio_When_Description_Is_Empty()
        {
            var updatePortfolio = new UpdatePortfolio(name: "aaa",
                description: "", customerId: 1);

            var result = validatorUpdate.TestValidate(updatePortfolio);

            result.ShouldHaveValidationErrorFor(x => x.Description);
        }
    }
}
