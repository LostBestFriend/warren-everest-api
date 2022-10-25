using AppModels.AppModels.Orders;
using FluentValidation;

namespace AppServices.Validator.Orders
{
    public class CreateOrderValidator : AbstractValidator<CreateOrder>
    {
        public CreateOrderValidator()
        {
            RuleFor(order => order.Quotes)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(order => order.NetValue)
                .NotEmpty();

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
