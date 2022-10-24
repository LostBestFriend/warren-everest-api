using AppModels.AppModels.Products;
using FluentValidation;

namespace AppServices.Validator.Products
{
    public class CreateProductValidator : AbstractValidator<CreateProduct>
    {
        public CreateProductValidator()
        {
            RuleFor(product => product.Symbol)
                .NotEmpty();

            RuleFor(product => product.UnitPrice)
                .GreaterThan(0)
                .NotEmpty();

            RuleFor(product => product.IssuanceAt)
                .NotEmpty();

            RuleFor(product => product.ExpirationAt)
                .NotEmpty()
                .GreaterThan(product => product.IssuanceAt);

            RuleFor(product => product.Type)
                .IsInEnum()
                .NotEmpty();
        }
    }
}
