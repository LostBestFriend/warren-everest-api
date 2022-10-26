using AppModels.AppModels.Products;
using AppModels.EnumModels;
using AutoMapper;
using DomainModels.Models;
using FluentAssertions;
using System;
using Xunit;

namespace AppServices.Tests.Profiles
{
    public class ProductProfileTest
    {
        private readonly IMapper _mapperUpdate;
        private readonly IMapper _mapperResponse;
        private readonly IMapper _mapperCreate;

        public ProductProfileTest()
        {
            _mapperUpdate = new MapperConfiguration(cfg =>
            cfg.CreateMap<UpdateProduct, Product>()).CreateMapper();
            _mapperResponse = new MapperConfiguration(cfg =>
            cfg.CreateMap<Product, ProductResponse>().ReverseMap()).CreateMapper();
            _mapperCreate = new MapperConfiguration(cfg =>
            cfg.CreateMap<CreateProduct, Product>()).CreateMapper();
        }

        [Fact]
        public void Should_Map_UpdateProduct_Sucessfully()
        {
            var product = new Product(symbol: "aaa",
                issuanceAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                expirationAt: new DateTime(year: 2200, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                type: DomainModels.Enums.ProductType.FixedIncome,
                unitPrice: 1);

            var updateProduct = new UpdateProduct(symbol: "aaa",
                issuanceAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                expirationAt: new DateTime(year: 2200, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                type: ProductType.FixedIncome,
                unitPrice: 1);

            var result = _mapperUpdate.Map<Product>(updateProduct);

            result.Should().BeEquivalentTo(product);
        }

        [Fact]
        public void Should_Map_CreateProduct_Sucessfully()
        {
            var product = new Product(symbol: "aaa",
                issuanceAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                expirationAt: new DateTime(year: 2200, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                type: DomainModels.Enums.ProductType.FixedIncome,
                unitPrice: 1);
            var createProduct = new CreateProduct(symbol: "aaa",
                issuanceAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                expirationAt: new DateTime(year: 2200, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                type: ProductType.FixedIncome,
                unitPrice: 1);

            var result = _mapperCreate.Map<Product>(createProduct);

            result.Should().BeEquivalentTo(product);
        }

        [Fact]
        public void Should_Map_ProductResponse_Sucessfully()
        {
            var product = new Product(symbol: "aaa",
                issuanceAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                expirationAt: new DateTime(year: 2200, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                type: DomainModels.Enums.ProductType.FixedIncome,
                unitPrice: 1);
            var productResponse = new ProductResponse(id: 1,
                symbol: "aaa", issuanceAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                expirationAt: new DateTime(year: 2200, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                type: ProductType.FixedIncome,
                unitPrice: 1);

            var result = _mapperResponse.Map<ProductResponse>(product);

            result.Should().NotBeNull();
        }
        [Fact]
        public void Should_Map_ProductResponse_Reverse_Sucessfully()
        {
            var product = new Product(symbol: "aaa",
                issuanceAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                expirationAt: new DateTime(year: 2200, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                type: DomainModels.Enums.ProductType.FixedIncome,
                unitPrice: 1);
            var productResponse = new ProductResponse(id: 1,
                symbol: "aaa", issuanceAt: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                expirationAt: new DateTime(year: 2200, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                type: ProductType.FixedIncome,
                unitPrice: 1);

            var result = _mapperResponse.Map<Product>(productResponse);

            result.Should().NotBeNull();
        }
    }
}
