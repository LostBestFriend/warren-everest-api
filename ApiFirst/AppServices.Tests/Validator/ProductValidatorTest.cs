using ApiFirst.Tests.Fixtures.AppServices.Product;
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
            var createProduct = CreateProductFixture.GenerateCreateProductFixture();

            var result = validatorCreate.Validate(createProduct);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Not_Validate_CreateProduct_When_Symbol_Is_Empty()
        {
            var createProduct = CreateProductFixture.GenerateCreateProductFixture();
            createProduct.Symbol = "";

            var result = validatorCreate.TestValidate(createProduct);

            result.ShouldHaveValidationErrorFor(x => x.Symbol);
        }

        [Fact]
        public void Should_Not_Validate_CreateProduct_When_IssuanceAt_Is_Empty()
        {
            var createProduct = CreateProductFixture.GenerateCreateProductFixture();
            createProduct.IssuanceAt = new DateTime();

            var result = validatorCreate.TestValidate(createProduct);

            result.ShouldHaveValidationErrorFor(x => x.IssuanceAt);
        }

        [Fact]
        public void Should_Not_Validate_CreateProduct_When_ExpirationAt_Is_Empty()
        {
            var createProduct = CreateProductFixture.GenerateCreateProductFixture();
            createProduct.ExpirationAt = new DateTime();

            var result = validatorCreate.TestValidate(createProduct);

            result.ShouldHaveValidationErrorFor(x => x.ExpirationAt);
        }

        [Fact]
        public void Should_Not_Validate_CreateProduct_When_ExpirationAt_Is_Lesser_Than_IssuanceAt()
        {
            var createProduct = CreateProductFixture.GenerateCreateProductFixture();
            createProduct.IssuanceAt = new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2);
            createProduct.ExpirationAt = new DateTime(year: 2000, month: 2, day: 2, hour: 14, minute: 22, second: 2);

            var result = validatorCreate.TestValidate(createProduct);

            result.ShouldHaveValidationErrorFor(x => x.ExpirationAt);
        }

        [Fact]
        public void Should_Not_Validate_CreateProduct_When_Type_Is_Empty()
        {
            var createProduct = CreateProductFixture.GenerateCreateProductFixture();
            createProduct.Type = 0;

            var result = validatorCreate.TestValidate(createProduct);

            result.ShouldHaveValidationErrorFor(x => x.Type);
        }

        [Fact]
        public void Should_Not_Validate_CreateProduct_When_UnitPrice_Is_Lesser_Than_One()
        {
            var createProduct = CreateProductFixture.GenerateCreateProductFixture();
            createProduct.UnitPrice = 0;

            var result = validatorCreate.TestValidate(createProduct);

            result.ShouldHaveValidationErrorFor(x => x.UnitPrice);
        }

        [Fact]
        public void Should_Validate_UpdateProduct_Sucessfully()
        {
            var updateProduct = UpdateProductFixture.GenerateUpdateProductFixture();

            var result = validatorUpdate.Validate(updateProduct);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Not_Validate_UpdateProduct_When_Symbol_Is_Empty()
        {
            var updateProduct = UpdateProductFixture.GenerateUpdateProductFixture();
            updateProduct.Symbol = "";

            var result = validatorUpdate.TestValidate(updateProduct);

            result.ShouldHaveValidationErrorFor(x => x.Symbol);
        }

        [Fact]
        public void Should_Not_Validate_UpdateProduct_When_IssuanceAt_Is_Empty()
        {
            var updateProduct = UpdateProductFixture.GenerateUpdateProductFixture();
            updateProduct.IssuanceAt = new DateTime();

            var result = validatorUpdate.TestValidate(updateProduct);

            result.ShouldHaveValidationErrorFor(x => x.IssuanceAt);
        }

        [Fact]
        public void Should_Not_Validate_UpdateProduct_When_ExpirationAt_Is_Empty()
        {
            var updateProduct = UpdateProductFixture.GenerateUpdateProductFixture();
            updateProduct.ExpirationAt = new DateTime();

            var result = validatorUpdate.TestValidate(updateProduct);

            result.ShouldHaveValidationErrorFor(x => x.ExpirationAt);
        }

        [Fact]
        public void Should_Not_Validate_UpdateProduct_When_ExpirationAt_Is_Lesser_Than_IssuanceAt()
        {
            var updateProduct = UpdateProductFixture.GenerateUpdateProductFixture();
            updateProduct.IssuanceAt = new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2);
            updateProduct.ExpirationAt = new DateTime(year: 2000, month: 2, day: 2, hour: 14, minute: 22, second: 2);

            var result = validatorUpdate.TestValidate(updateProduct);

            result.ShouldHaveValidationErrorFor(x => x.ExpirationAt);
        }

        [Fact]
        public void Should_Not_Validate_UpdateProduct_When_Type_Is_Empty()
        {
            var updateProduct = UpdateProductFixture.GenerateUpdateProductFixture();
            updateProduct.Type = 0;

            var result = validatorUpdate.TestValidate(updateProduct);

            result.ShouldHaveValidationErrorFor(x => x.Type);
        }

        [Fact]
        public void Should_Not_Validate_UpdateProduct_When_UnitPrice_Is_Lesser_Than_One()
        {
            var updateProduct = UpdateProductFixture.GenerateUpdateProductFixture();
            updateProduct.UnitPrice = 0;

            var result = validatorUpdate.TestValidate(updateProduct);

            result.ShouldHaveValidationErrorFor(x => x.UnitPrice);
        }
    }
}
