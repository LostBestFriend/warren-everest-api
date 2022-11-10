using AppModels.AppModels.Orders;
using AppModels.EnumModels;
using AppServices.Validator.Orders;
using FluentAssertions;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace AppServices.Tests.Validator
{
    public class OrderValidatorTest
    {

        public readonly CreateOrderValidator validatorCreate;
        public readonly UpdateOrderValidator validatorUpdate;

        public OrderValidatorTest()
        {
            validatorCreate = new CreateOrderValidator();
            validatorUpdate = new UpdateOrderValidator();
        }

        [Fact]
        public void Should_Validate_CreateOrder_Sucessfully()
        {
            var createOrder = new CreateOrder(quotes: 1, unitPrice: 1, liquidatedAt:
                new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                direction: OrderDirection.Buy,
                productId: 1, portfolioId: 1);

            var result = validatorCreate.Validate(createOrder);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Not_Validate_CreateOrder_When_Quotes_Is_Lesser_Than_One()
        {
            var createOrder = new CreateOrder(quotes: 0, unitPrice: 1, liquidatedAt:
                new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                direction: OrderDirection.Buy,
                productId: 1, portfolioId: 1);

            var result = validatorCreate.TestValidate(createOrder);

            result.ShouldHaveValidationErrorFor(x => x.Quotes);
        }


        [Fact]
        public void Should_Not_Validate_CreateOrder_When_Quotes_Is_Empty()
        {
            var createOrder = new CreateOrder(quotes: int.MinValue, unitPrice: 1, liquidatedAt:
                new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                direction: OrderDirection.Buy,
                productId: 1, portfolioId: 1);

            var result = validatorCreate.TestValidate(createOrder);

            result.ShouldHaveValidationErrorFor(x => x.Quotes);
        }

        [Fact]
        public void Should_Not_Validate_CreateOrder_When_LiquidatedAt_Is_Empty()
        {
            var createOrder = new CreateOrder(quotes: 1, unitPrice: 1, liquidatedAt:
                DateTime.MinValue,
                direction: OrderDirection.Buy,
                productId: 1, portfolioId: 1);

            var result = validatorCreate.TestValidate(createOrder);

            result.ShouldHaveValidationErrorFor(x => x.LiquidatedAt);
        }

        [Fact]
        public void Should_Not_Validate_CreateOrder_When_Direction_Is_Empty()
        {
            var createOrder = new CreateOrder(quotes: 1, unitPrice: 1, liquidatedAt:
                DateTime.MinValue,
                direction: 0,
                productId: 1, portfolioId: 1);

            var result = validatorCreate.TestValidate(createOrder);

            result.ShouldHaveValidationErrorFor(x => x.Direction);
        }

        [Fact]
        public void Should_Validate_UpdateOrder_Sucessfully()
        {
            var updateOrder = new UpdateOrder(quotes: 1, unitPrice: 1, liquidatedAt:
                new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                direction: OrderDirection.Buy,
                productId: 1, portfolioId: 1);

            var result = validatorUpdate.Validate(updateOrder);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Not_Validate_UpdateOrder_When_Quotes_Is_Lesser_Than_One()
        {
            var updateOrder = new UpdateOrder(quotes: 0, unitPrice: 1, liquidatedAt:
                new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                direction: OrderDirection.Buy,
                productId: 1, portfolioId: 1);

            var result = validatorUpdate.TestValidate(updateOrder);

            result.ShouldHaveValidationErrorFor(x => x.Quotes);
        }


        [Fact]
        public void Should_Not_Validate_UpdateOrder_When_Quotes_Is_Empty()
        {
            var updateOrder = new UpdateOrder(quotes: int.MinValue, unitPrice: 1, liquidatedAt:
                new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                direction: OrderDirection.Buy,
                productId: 1, portfolioId: 1);

            var result = validatorUpdate.TestValidate(updateOrder);

            result.ShouldHaveValidationErrorFor(x => x.Quotes);
        }

        [Fact]
        public void Should_Not_Validate_UpdateOrder_When_LiquidatedAt_Is_Empty()
        {
            var updateOrder = new UpdateOrder(quotes: 1, unitPrice: 1, liquidatedAt:
                DateTime.MinValue,
                direction: OrderDirection.Buy,
                productId: 1, portfolioId: 1);

            var result = validatorUpdate.TestValidate(updateOrder);

            result.ShouldHaveValidationErrorFor(x => x.LiquidatedAt);
        }

        [Fact]
        public void Should_Not_Validate_UpdateOrder_When_Direction_Is_Empty()
        {
            var updateOrder = new UpdateOrder(quotes: 1, unitPrice: 1, liquidatedAt:
                DateTime.MinValue,
                direction: 0,
                productId: 1, portfolioId: 1);

            var result = validatorUpdate.TestValidate(updateOrder);

            result.ShouldHaveValidationErrorFor(x => x.Direction);
        }
    }
}
