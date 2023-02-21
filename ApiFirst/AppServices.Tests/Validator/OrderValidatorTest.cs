using ApiFirst.Tests.Fixtures.AppServices.Order;
using AppServices.Validator.Orders;
using FluentAssertions;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace ApiFirst.Tests.Validator
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
            var createOrder = CreateOrderFixture.GenerateCreateOrderFixture();

            var result = validatorCreate.Validate(createOrder);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Not_Validate_CreateOrder_When_Quotes_Is_Lesser_Than_One()
        {
            var createOrder = CreateOrderFixture.GenerateCreateOrderFixture();
            createOrder.Quotes = 0;

            var result = validatorCreate.TestValidate(createOrder);

            result.ShouldHaveValidationErrorFor(x => x.Quotes);
        }


        [Fact]
        public void Should_Not_Validate_CreateOrder_When_Quotes_Is_Empty()
        {
            var createOrder = CreateOrderFixture.GenerateCreateOrderFixture();
            createOrder.Quotes = int.MinValue;

            var result = validatorCreate.TestValidate(createOrder);

            result.ShouldHaveValidationErrorFor(x => x.Quotes);
        }

        [Fact]
        public void Should_Not_Validate_CreateOrder_When_LiquidatedAt_Is_Empty()
        {
            var createOrder = CreateOrderFixture.GenerateCreateOrderFixture();
            createOrder.LiquidatedAt = new DateTime();

            var result = validatorCreate.TestValidate(createOrder);

            result.ShouldHaveValidationErrorFor(x => x.LiquidatedAt);
        }

        [Fact]
        public void Should_Not_Validate_CreateOrder_When_Direction_Is_Empty()
        {
            var createOrder = CreateOrderFixture.GenerateCreateOrderFixture();
            createOrder.Direction = 0;

            var result = validatorCreate.TestValidate(createOrder);

            result.ShouldHaveValidationErrorFor(x => x.Direction);
        }

        [Fact]
        public void Should_Validate_UpdateOrder_Sucessfully()
        {
            var updateOrder = UpdateOrderFixture.GenerateUpdateOrderFixture();

            var result = validatorUpdate.TestValidate(updateOrder);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_Not_Validate_UpdateOrder_When_Quotes_Is_Lesser_Than_One()
        {
            var updateOrder = UpdateOrderFixture.GenerateUpdateOrderFixture();
            updateOrder.Quotes = 0;

            var result = validatorUpdate.TestValidate(updateOrder);

            result.ShouldHaveValidationErrorFor(x => x.Quotes);
        }


        [Fact]
        public void Should_Not_Validate_UpdateOrder_When_Quotes_Is_Empty()
        {
            var updateOrder = UpdateOrderFixture.GenerateUpdateOrderFixture();
            updateOrder.Quotes = int.MinValue;

            var result = validatorUpdate.TestValidate(updateOrder);

            result.ShouldHaveValidationErrorFor(x => x.Quotes);
        }

        [Fact]
        public void Should_Not_Validate_UpdateOrder_When_LiquidatedAt_Is_Empty()
        {
            var updateOrder = UpdateOrderFixture.GenerateUpdateOrderFixture();
            updateOrder.LiquidatedAt = new DateTime();

            var result = validatorUpdate.TestValidate(updateOrder);

            result.ShouldHaveValidationErrorFor(x => x.LiquidatedAt);
        }

        [Fact]
        public void Should_Not_Validate_UpdateOrder_When_Direction_Is_Empty()
        {
            var updateOrder = UpdateOrderFixture.GenerateUpdateOrderFixture();
            updateOrder.Direction = 0;

            var result = validatorUpdate.TestValidate(updateOrder);

            result.ShouldHaveValidationErrorFor(x => x.Direction);
        }
    }
}
