using AppModels.AppModels.Portfolio;
using FluentValidation;

namespace AppServices.Validator.Portfolio
{
    public class CreatePortfolioValidator : AbstractValidator<CreatePortfolio>
    {
        public CreatePortfolioValidator()
        {
            RuleFor(portfolio => portfolio.Name)
                .NotEmpty();

            RuleFor(portfolio => portfolio.Description)
                .NotEmpty();

            RuleFor(portfolio => portfolio.CustomerId)
                .NotEmpty();
        }
    }
}
