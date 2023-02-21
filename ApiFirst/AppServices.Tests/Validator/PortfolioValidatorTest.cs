using ApiFirst.Tests.Fixtures.AppServices.Portfolio;
using AppServices.Validator.Portfolios;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace ApiFirst.Tests.Validator
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
            var createPortfolio = CreatePortfolioFixture.GenerateCreatePortfolioFixture();

            var result = validatorCreate.Validate(createPortfolio);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Not_Validate_CreatePortfolio_When_Name_Is_Empty()
        {
            var createPortfolio = CreatePortfolioFixture.GenerateCreatePortfolioFixture();
            createPortfolio.Name = "";

            var result = validatorCreate.TestValidate(createPortfolio);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Not_Validate_CreatePortfolio_When_Description_Is_Empty()
        {
            var createPortfolio = CreatePortfolioFixture.GenerateCreatePortfolioFixture();
            createPortfolio.Description = "";

            var result = validatorCreate.TestValidate(createPortfolio);

            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Should_Validate_UpdatePortfolio_Sucessfully()
        {
            var updatePortfolio = UpdatePortfolioFixture.GenerateUpdatePortfolioFixture();

            var result = validatorUpdate.Validate(updatePortfolio);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Not_Validate_UpdatePortfolio_When_Name_Is_Empty()
        {
            var updatePortfolio = UpdatePortfolioFixture.GenerateUpdatePortfolioFixture();
            updatePortfolio.Name = "";

            var result = validatorUpdate.TestValidate(updatePortfolio);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Not_Validate_UpdatePortfolio_When_Description_Is_Empty()
        {
            var updatePortfolio = UpdatePortfolioFixture.GenerateUpdatePortfolioFixture();
            updatePortfolio.Description = "";

            var result = validatorUpdate.TestValidate(updatePortfolio);

            result.ShouldHaveValidationErrorFor(x => x.Description);
        }
    }
}
