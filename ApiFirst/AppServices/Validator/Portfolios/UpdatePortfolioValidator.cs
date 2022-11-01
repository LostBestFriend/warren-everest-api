using AppModels.AppModels.Portfolios;
using FluentValidation;

namespace AppServices.Validator.Portfolios
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
