using AppModels.AppModels.Products;
using AppModels.EnumModels;
using AppServices.Profiles;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Tests.Fixtures;
using FluentAssertions;
using Xunit;

namespace AppServices.Tests.Profiles
{
    public class ProductProfileTest
    {
        private readonly IMapper _mapper;

        public ProductProfileTest()
        {
            _mapper = new MapperConfiguration(cfg =>
            cfg.AddProfile<ProductProfile>()).CreateMapper();
        }

        [Fact]
        public void Should_Map_UpdateProduct_Sucessfully()
        {
            var product = ProductFixture.GenerateProductFixture();

            var updateProduct = new UpdateProduct(
               symbol: product.Symbol,
               issuanceAt: product.IssuanceAt,
               expirationAt: product.ExpirationAt,
               type: ProductType.FixedIncome,
               unitPrice: product.UnitPrice);

            var result = _mapper.Map<Product>(updateProduct);

            result.Should().BeEquivalentTo(product);
        }

        [Fact]
        public void Should_Map_CreateProduct_Sucessfully()
        {
            var product = ProductFixture.GenerateProductFixture();

            var createProduct = new CreateProduct(
                symbol: product.Symbol,
                issuanceAt: product.IssuanceAt,
                expirationAt: product.ExpirationAt,
                type: ProductType.FixedIncome,
                unitPrice: product.UnitPrice);

            var result = _mapper.Map<Product>(createProduct);

            result.Should().BeEquivalentTo(product);
        }

        [Fact]
        public void Should_Map_ProductResponse_Sucessfully()
        {
            var product = ProductFixture.GenerateProductFixture();

            var productResponse = new ProductResponse(
                id: 1,
                symbol: product.Symbol,
                issuanceAt: product.IssuanceAt,
                expirationAt: product.ExpirationAt,
                type: ProductType.FixedIncome,
                unitPrice: product.UnitPrice);

            var result = _mapper.Map<ProductResponse>(product);

            result.Should().NotBeNull();
        }

        [Fact]
        public void Should_Map_ProductResponse_Reverse_Sucessfully()
        {
            var product = ProductFixture.GenerateProductFixture();

            var productResponse = new ProductResponse(
                id: 1,
                symbol: product.Symbol,
                issuanceAt: product.IssuanceAt,
                expirationAt: product.ExpirationAt,
                type: ProductType.FixedIncome,
                unitPrice: product.UnitPrice);

            var result = _mapper.Map<Product>(productResponse);

            result.Should().BeEquivalentTo(productResponse);
        }
    }
}
