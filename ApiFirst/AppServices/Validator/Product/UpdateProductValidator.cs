using AppModels.AppModels.Product;
using FluentValidation;

namespace AppServices.Validator.Product
{
    public class UpdateProductValidator : AbstractValidator<UpdateProduct>
    {
        public UpdateProductValidator()
        {
            RuleFor(product => product.Symbol)
                .NotEmpty();

            RuleFor(product => product.UnitPrice)
                .NotEmpty();

            RuleFor(product => product.IssuanceAt)
                .NotEmpty();

            RuleFor(product => product.ExpirationAt)
                .NotEmpty()
                .GreaterThan(product => product.IssuanceAt);

            RuleFor(product => product.Type)
                .NotEmpty();
        }
    }
}
