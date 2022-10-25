using AppModels.AppModels.Products;
using AppModels.EnumModels;
using AutoMapper;
using DomainModels.Models;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace AppServices.Tests.Profiles
{
    public class ProductProfileTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IMapper> _mapperMock;

        public ProductProfileTest()
        {
            _mapperMock = new();
            _mapper = _mapperMock.Object;
        }

        [Fact]
        public void Should_Map_UpdateProduct_Sucessfully()
        {
            var product = new Product(symbol: "aaa",
                issuanceAt: DateTime.Now.AddDays(-2),
                expirationAt: DateTime.Now.AddDays(2),
                type: DomainModels.Enums.ProductType.FixedIncome,
                unitPrice: 1);

            var updateProduct = new UpdateProduct(symbol: "aaa",
                issuanceAt: DateTime.Now.AddDays(-2),
                expirationAt: DateTime.Now.AddDays(2),
                type: ProductType.FixedIncome,
                unitPrice: 1);

            _mapperMock.Setup(p => p.Map<Product>(It.IsAny<UpdateProduct>())).Returns(product);

            var result = _mapper.Map<Product>(updateProduct);

            result.Should().BeEquivalentTo(product);

            _mapperMock.Verify(p => p.Map<Product>(It.IsAny<UpdateProduct>()), Times.Once);
        }

        [Fact]
        public void Should_Map_CreateProduct_Sucessfully()
        {
            var product = new Product(symbol: "aaa",
                issuanceAt: DateTime.Now.AddDays(-2),
                expirationAt: DateTime.Now.AddDays(2),
                type: DomainModels.Enums.ProductType.FixedIncome,
                unitPrice: 1);
            var createProduct = new CreateProduct(symbol: "aaa",
                issuanceAt: DateTime.Now.AddDays(-2),
                expirationAt: DateTime.Now.AddDays(2),
                type: ProductType.FixedIncome,
                unitPrice: 1);

            _mapperMock.Setup(p => p.Map<Product>(It.IsAny<CreateProduct>())).Returns(product);

            var result = _mapper.Map<Product>(createProduct);

            _mapperMock.Verify(p => p.Map<Product>(It.IsAny<CreateProduct>()), Times.Once);

            result.Should().BeEquivalentTo(product);
        }

        [Fact]
        public void Should_Map_ProductResponse_Sucessfully()
        {
            var product = new Product(symbol: "aaa",
                issuanceAt: DateTime.Now.AddDays(-2),
                expirationAt: DateTime.Now.AddDays(2),
                type: DomainModels.Enums.ProductType.FixedIncome,
                unitPrice: 1);
            var productResponse = new ProductResponse(id: 1,
                symbol: "aaa", issuanceAt: DateTime.Now.AddDays(-2),
                expirationAt: DateTime.Now.AddDays(2),
                type: ProductType.FixedIncome,
                unitPrice: 1);

            _mapperMock.Setup(p => p.Map<ProductResponse>(It.IsAny<Product>())).Returns(productResponse);

            var result = _mapper.Map<ProductResponse>(product);

            result.Should().BeEquivalentTo(productResponse);

            _mapperMock.Verify(p => p.Map<ProductResponse>(It.IsAny<Product>()), Times.Once);
        }
        [Fact]
        public void Should_Map_ProductResponse_Reverse_Sucessfully()
        {
            var product = new Product(symbol: "aaa",
                issuanceAt: DateTime.Now.AddDays(-2),
                expirationAt: DateTime.Now.AddDays(2),
                type: DomainModels.Enums.ProductType.FixedIncome,
                unitPrice: 1);
            var productResponse = new ProductResponse(id: 1,
                symbol: "aaa", issuanceAt: DateTime.Now.AddDays(-2),
                expirationAt: DateTime.Now.AddDays(2),
                type: ProductType.FixedIncome,
                unitPrice: 1);

            _mapperMock.Setup(p => p.Map<Product>(It.IsAny<ProductResponse>())).Returns(product);

            var result = _mapper.Map<Product>(productResponse);

            result.Should().BeEquivalentTo(product);

            _mapperMock.Verify(p => p.Map<Product>(It.IsAny<ProductResponse>()), Times.Once);
        }
    }
}
