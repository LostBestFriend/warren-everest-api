using AppModels.AppModels.Products;
using AppServices.Services;
using AppServices.Tests.Fixtures.Product;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using DomainServices.Tests.Fixtures;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace AppServices.Tests.Services
{
    public class ProductAppServiceTest
    {
        private readonly ProductAppService _productAppService;
        private readonly Mock<IProductService> _productServiceMock;

        public ProductAppServiceTest()
        {
            IMapper _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateProduct, Product>();
                cfg.CreateMap<Product, ProductResponse>().ReverseMap();
                cfg.CreateMap<CreateProduct, Product>();
            }).CreateMapper();
            _productServiceMock = new();
            _productAppService = new ProductAppService(_mapper, _productServiceMock.Object);
        }

        [Fact]
        public async Task Should_GetAllAsync_Sucessfully()
        {
            var products = ProductFixture.GenerateProductFixture(3);
            var productResponses = ProductResponseFixture.GenerateProductResponseFixture(3);

            _productServiceMock.Setup(p => p.GetAllAsync()).ReturnsAsync(products);

            var result = await _productAppService.GetAllAsync();

            result.Should().NotBeNull();

            _productServiceMock.Verify(p => p.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async void Should_GetByIdAsync_Sucessfully()
        {
            long id = 1;
            var product = ProductFixture.GenerateProductFixture();
            var productResponse = ProductResponseFixture.GenerateProductResponseFixture();

            _productServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(product);

            var result = await _productAppService.GetByIdAsync(id);

            result.Should().NotBeNull();

            _productServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async void Should_CreateAsync_Sucessfully()
        {
            var createProduct = CreateProductFixture.GenerateCreateProductFixture();
            var product = ProductFixture.GenerateProductFixture();
            var id = 1;

            _productServiceMock.Setup(p => p.CreateAsync(It.IsAny<Product>())).ReturnsAsync(id);

            var result = await _productAppService.CreateAsync(createProduct);
            result.Should().BeGreaterThanOrEqualTo(0);

            _productServiceMock.Verify(p => p.CreateAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void Should_Update_Sucessfully()
        {
            long id = 1;
            var updateProduct = UpdateProductFixture.GenerateUpdateProductFixture();

            _productServiceMock.Setup(p => p.Update(It.IsAny<Product>()));

            _productAppService.Update(id, updateProduct);

            _productServiceMock.Verify(p => p.Update(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void Should_Delete_Sucessfully()
        {
            long id = 1;

            _productServiceMock.Setup(p => p.DeleteAsync(It.IsAny<long>()));

            _productAppService.Delete(id);

            _productServiceMock.Verify(p => p.DeleteAsync(It.IsAny<long>()), Times.Once);
        }
    }
}
