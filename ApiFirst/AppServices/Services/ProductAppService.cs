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
        public IEnumerable<ProductResponse> GetAll()
        {
            var result = _productService.GetAll();
            return _mapper.Map<IEnumerable<ProductResponse>>(result);
        }

        public async Task<ProductResponse> GetByIdAsync(long id)
        {
            var result = await _productService.GetByIdAsync(id);
            return _mapper.Map<ProductResponse>(result);
        }

        public async Task<long> CreateAsync(CreateProduct model)
        {
            Product product = _mapper.Map<Product>(model);
            return await _productService.CreateAsync(product);
        }

        public void Update(long productId, UpdateProduct model)
        {
            Product product = _mapper.Map<Product>(model);
            product.Id = productId;
            _productService.Update(product);
        }

        public void Delete(long id)
        {
            _productService.Delete(id);
        }
    }
}
