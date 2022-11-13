using AppModels.AppModels.Products;
using AppModels.EnumModels;
using AppServices.Validator.Products;
using FluentAssertions;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace ApiFirst.Tests.Validator
{
    public class ProductValidatorTest
    {
        public readonly CreateProductValidator validatorCreate;
        public readonly UpdateProductValidator validatorUpdate;

        public ProductValidatorTest()
        {
            validatorCreate = new CreateProductValidator();
            validatorUpdate = new UpdateProductValidator();
        }

        [Fact]
        public void Should_Validate_CreateProduct_Sucessfully()
        {
            var createProduct = new CreateProduct(symbol: "aaa",
                issuanceAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                expirationAt: new DateTime(year: 2200, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                type: ProductType.FixedIncome,
                unitPrice: 1);

            var result = validatorCreate.Validate(createProduct);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Not_Validate_CreateProduct_When_Symbol_Is_Empty()
        {
            var createProduct = new CreateProduct(symbol: "",
                issuanceAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                expirationAt: new DateTime(year: 2200, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                type: ProductType.FixedIncome,
                unitPrice: 1);

            var result = validatorCreate.TestValidate(createProduct);

            result.ShouldHaveValidationErrorFor(x => x.Symbol);
        }

        [Fact]
        public void Should_Not_Validate_CreateProduct_When_IssuanceAt_Is_Empty()
        {
            var createProduct = new CreateProduct(symbol: "aaa",
                issuanceAt: DateTime.MinValue,
                expirationAt: new DateTime(year: 2200, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                type: ProductType.FixedIncome,
                unitPrice: 1);

            var result = validatorCreate.TestValidate(createProduct);

            result.ShouldHaveValidationErrorFor(x => x.IssuanceAt);
        }

        [Fact]
        public void Should_Not_Validate_CreateProduct_When_ExpirationAt_Is_Empty()
        {
            var createProduct = new CreateProduct(symbol: "aaa",
                issuanceAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                expirationAt: DateTime.MinValue,
                type: ProductType.FixedIncome,
                unitPrice: 1);

            var result = validatorCreate.TestValidate(createProduct);

            result.ShouldHaveValidationErrorFor(x => x.ExpirationAt);
        }

        [Fact]
        public void Should_Not_Validate_CreateProduct_When_ExpirationAt_Is_Lesser_Than_IssuanceAt()
        {
            var createProduct = new CreateProduct(symbol: "aaa",
                issuanceAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                expirationAt: new DateTime(year: 2000, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                type: ProductType.FixedIncome,
                unitPrice: 1);

            var result = validatorCreate.TestValidate(createProduct);

            result.ShouldHaveValidationErrorFor(x => x.ExpirationAt);
        }

        [Fact]
        public void Should_Not_Validate_CreateProduct_When_Type_Is_Empty()
        {
            var createProduct = new CreateProduct(symbol: "aaa",
                issuanceAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                expirationAt: new DateTime(year: 2200, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                type: 0,
                unitPrice: 1);

            var result = validatorCreate.TestValidate(createProduct);

            result.ShouldHaveValidationErrorFor(x => x.Type);
        }

        [Fact]
        public void Should_Not_Validate_CreateProduct_When_UnitPrice_Is_Lesser_Than_One()
        {
            var createProduct = new CreateProduct(symbol: "aaa",
                issuanceAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                expirationAt: new DateTime(year: 2200, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                type: ProductType.FixedIncome,
                unitPrice: 0);

            var result = validatorCreate.TestValidate(createProduct);

            result.ShouldHaveValidationErrorFor(x => x.UnitPrice);
        }

        [Fact]
        public void Should_Validate_UpdateProduct_Sucessfully()
        {
            var updateProduct = new UpdateProduct(symbol: "aaa",
                issuanceAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                expirationAt: new DateTime(year: 2200, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                type: ProductType.FixedIncome,
                unitPrice: 1);

            var result = validatorUpdate.Validate(updateProduct);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Not_Validate_UpdateProduct_When_Symbol_Is_Empty()
        {
            var updateProduct = new UpdateProduct(symbol: "",
                issuanceAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                expirationAt: new DateTime(year: 2200, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                type: ProductType.FixedIncome,
                unitPrice: 1);

            var result = validatorUpdate.TestValidate(updateProduct);

            result.ShouldHaveValidationErrorFor(x => x.Symbol);
        }

        [Fact]
        public void Should_Not_Validate_UpdateProduct_When_IssuanceAt_Is_Empty()
        {
            var updateProduct = new UpdateProduct(symbol: "aaa",
                issuanceAt: DateTime.MinValue,
                expirationAt: new DateTime(year: 2200, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                type: ProductType.FixedIncome,
                unitPrice: 1);

            var result = validatorUpdate.TestValidate(updateProduct);

            result.ShouldHaveValidationErrorFor(x => x.IssuanceAt);
        }

        [Fact]
        public void Should_Not_Validate_UpdateProduct_When_ExpirationAt_Is_Empty()
        {
            var updateProduct = new UpdateProduct(symbol: "aaa",
                issuanceAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                expirationAt: DateTime.MinValue,
                type: ProductType.FixedIncome,
                unitPrice: 1);

            var result = validatorUpdate.TestValidate(updateProduct);

            result.ShouldHaveValidationErrorFor(x => x.ExpirationAt);
        }

        [Fact]
        public void Should_Not_Validate_UpdateProduct_When_ExpirationAt_Is_Lesser_Than_IssuanceAt()
        {
            var updateProduct = new UpdateProduct(symbol: "aaa",
                issuanceAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                expirationAt: new DateTime(year: 2000, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                type: ProductType.FixedIncome,
                unitPrice: 1);

            var result = validatorUpdate.TestValidate(updateProduct);

            result.ShouldHaveValidationErrorFor(x => x.ExpirationAt);
        }

        [Fact]
        public void Should_Not_Validate_UpdateProduct_When_Type_Is_Empty()
        {
            var updateProduct = new UpdateProduct(symbol: "aaa",
                issuanceAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                expirationAt: new DateTime(year: 2200, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                type: 0,
                unitPrice: 1);

            var result = validatorUpdate.TestValidate(updateProduct);

            result.ShouldHaveValidationErrorFor(x => x.Type);
        }

        [Fact]
        public void Should_Not_Validate_UpdateProduct_When_UnitPrice_Is_Lesser_Than_One()
        {
            var updateProduct = new UpdateProduct(symbol: "aaa",
                issuanceAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                expirationAt: new DateTime(year: 2200, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                type: ProductType.FixedIncome,
                unitPrice: 0);

            var result = validatorUpdate.TestValidate(updateProduct);

            result.ShouldHaveValidationErrorFor(x => x.UnitPrice);
        }
    }
}
