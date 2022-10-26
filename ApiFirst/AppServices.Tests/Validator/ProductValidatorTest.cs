using AppModels.AppModels.Products;
using AppModels.EnumModels;
using AppServices.Validator.Products;
using FluentAssertions;
using System;
using Xunit;

namespace AppServices.Tests.Validator
{
    public class ProductValidatorTest
    {
        [Fact]
        public void Should_Validate_CreateProduct_Sucessfully()
        {
            var createProduct = new CreateProduct(symbol: "aaa",
                issuanceAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                expirationAt: new DateTime(year: 2200, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                type: ProductType.FixedIncome,
                unitPrice: 1);
            var validator = new CreateProductValidator();

            var result = validator.Validate(createProduct);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Validate_UpdateProduct_Sucessfully()
        {
            var updateProduct = new UpdateProduct(symbol: "aaa",
                issuanceAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                expirationAt: new DateTime(year: 2200, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                type: ProductType.FixedIncome,
                unitPrice: 1);
            var validator = new UpdateProductValidator();
            var result = validator.Validate(updateProduct);

            result.IsValid.Should().BeTrue();
        }
    }
}
