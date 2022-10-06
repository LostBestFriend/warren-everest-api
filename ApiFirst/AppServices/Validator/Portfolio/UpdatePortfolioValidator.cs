using AppModels.AppModels.Portfolio;
using FluentValidation;

namespace AppServices.Validator.Portfolio
{
    public class UpdatePortfolioValidator : AbstractValidator<UpdatePortfolio>
    {
        public UpdatePortfolioValidator()
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
