using AppModels.AppModels.Orders;
using FluentValidation;

namespace AppServices.Validator.Orders
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrder>
    {
        public UpdateOrderValidator()
        {
            RuleFor(order => order.Quotes)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(order => order.Direction)
                .IsInEnum()
                .NotEmpty();

            RuleFor(order => order.LiquidateAt)
                .NotEmpty();

            RuleFor(order => order.PortfolioId)
                .NotEmpty();

            RuleFor(order => order.ProductId)
                .NotEmpty();
        }
    }
}
