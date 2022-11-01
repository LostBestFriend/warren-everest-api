using AppModels.AppModels.Products;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductAppService(IMapper mapper, IProductService productServices)
        {
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _productService = productServices ??
                throw new ArgumentNullException(nameof(productServices));
        }

        public async Task<IEnumerable<ProductResponse>> GetAllAsync()
        {
            var result = await _productService.GetAllAsync().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<ProductResponse>>(result);
        }

        public async Task<ProductResponse> GetByIdAsync(long id)
        {
            var result = await _productService.GetByIdAsync(id).ConfigureAwait(false);
            return _mapper.Map<ProductResponse>(result);
        }

        public async Task<long> CreateAsync(CreateProduct model)
        {
            var product = _mapper.Map<Product>(model);
            return await _productService.CreateAsync(product).ConfigureAwait(false);
        }

        public void Update(long productId, UpdateProduct model)
        {
            var product = _mapper.Map<Product>(model);
            product.Id = productId;
            _productService.Update(product);
        }

        public async Task DeleteAsync(long id)
        {
            await _productService.DeleteAsync(id).ConfigureAwait(false);
        }
    }
}
