using AppModels.AppModels.Product;
using AppServices.Services;
using AppServices.Tests.Fixtures.Product;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using DomainServices.Tests.Fixtures;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace AppServices.Tests.Services
{
    public class ProductAppServiceTest
    {
        private readonly ProductAppService _productAppService;
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<IMapper> _mapperMock;

        public ProductAppServiceTest()
        {
            _productServiceMock = new();
            _mapperMock = new();
            _productAppService = new ProductAppService(_mapperMock.Object, _productServiceMock.Object);
        }

        [Fact]
        public void Should_GetAll_Sucessfully()
        {
            var products = ProductFixture.GenerateProductFixture(3);
            var productResponses = ProductResponseFixture.GenerateProductResponseFixture(3);

            _productServiceMock.Setup(p => p.GetAll()).Returns(products);
            _mapperMock.Setup(p => p.Map<IEnumerable<ProductResponse>>(It.IsAny<IEnumerable<Product>>())).Returns(productResponses);

            var result = _productAppService.GetAll();

            result.Should().NotBeNull();

            _productServiceMock.Verify(p => p.GetAll(), Times.Once);
            _mapperMock.Verify(p => p.Map<IEnumerable<ProductResponse>>(It.IsAny<IEnumerable<Product>>()), Times.Once);
        }

        [Fact]
        public async void Should_GetByIdAsync_Sucessfully()
        {
            long id = 1;
            var product = ProductFixture.GenerateProductFixture();
            var productResponse = ProductResponseFixture.GenerateProductResponseFixture();

            _productServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(product);
            _mapperMock.Setup(p => p.Map<ProductResponse>(It.IsAny<Product>())).Returns(productResponse);

            var result = await _productAppService.GetByIdAsync(id);

            result.Should().NotBeNull();

            _productServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _mapperMock.Verify(p => p.Map<ProductResponse>(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async void Should_CreateAsync_Sucessfully()
        {
            var createProduct = CreateProductFixture.GenerateCreateProductFixture();
            var product = ProductFixture.GenerateProductFixture();
            var id = 1;

            _mapperMock.Setup(p => p.Map<Product>(It.IsAny<CreateProduct>())).Returns(product);
            _productServiceMock.Setup(p => p.CreateAsync(It.IsAny<Product>())).ReturnsAsync(id);

            var result = await _productAppService.CreateAsync(createProduct);
            result.Should().BeGreaterThanOrEqualTo(0);

            _mapperMock.Verify(p => p.Map<Product>(It.IsAny<CreateProduct>()), Times.Once);
            _productServiceMock.Verify(p => p.CreateAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void Should_Update_Sucessfully()
        {
            long id = 1;
            var product = ProductFixture.GenerateProductFixture();
            var updateProduct = UpdateProductFixture.GenerateUpdateProductFixture();

            _mapperMock.Setup(p => p.Map<Product>(It.IsAny<UpdateProduct>())).Returns(product);
            _productServiceMock.Setup(p => p.Update(It.IsAny<Product>()));

            _productAppService.Update(id, updateProduct);

            _mapperMock.Verify(p => p.Map<Product>(It.IsAny<UpdateProduct>()), Times.Once);
            _productServiceMock.Verify(p => p.Update(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void Should_Delete_Sucessfully()
        {
            long id = 1;

            _productServiceMock.Setup(p => p.Delete(It.IsAny<long>()));

            _productAppService.Delete(id);

            _productServiceMock.Verify(p => p.Delete(It.IsAny<long>()), Times.Once);
        }
    }
}
