using AppModels.AppModels.Orders;
using AppModels.EnumModels;
using AppServices.Validator.Orders;
using FluentAssertions;
using System;
using Xunit;

namespace AppServices.Tests.Validator
{
    public class OrderValidatorTest
    {
        [Fact]
        public void Should_Validate_CreateOrder_Sucessfully()
        {
            var createOrder = new CreateOrder(quotes: 1, unitPrice: 1, liquidatedAt:
                new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                direction: OrderDirection.Buy,
                productId: 1, portfolioId: 1);
            var validator = new CreateOrderValidator();

            var result = validator.Validate(createOrder);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Validate_UpdateOrder_Sucessfully()
        {
            var updateOrder = new UpdateOrder(quotes: 1, unitPrice: 1, liquidatedAt:
                new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                direction: OrderDirection.Buy,
                productId: 1, portfolioId: 1);
            var validator = new UpdateOrderValidator();
            var result = validator.Validate(updateOrder);

            result.IsValid.Should().BeTrue();
        }
    }
}
