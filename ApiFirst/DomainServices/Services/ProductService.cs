using DomainModels.Models;
using DomainServices.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork<WarrenContext> unitOfWork, IRepositoryFactory<WarrenContext> repository)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            _repositoryFactory = repository ?? (IRepositoryFactory)_unitOfWork;
        }

        public async Task<long> CreateAsync(Product model)
        {
            var repository = _unitOfWork.Repository<Product>();

            await repository.AddAsync(model).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return model.Id;
        }

        public async Task<Product> GetByIdAsync(long id)
        {
            var repository = _repositoryFactory.Repository<Product>();
            var query = repository.SingleResultQuery().AndFilter(product => product.Id == id);
            var product = await repository.FirstOrDefaultAsync(query).ConfigureAwait(false);

            if (product is null)
                throw new ArgumentNullException($"Produto não encontrado para o Id: {id}");

            return product;
        }


        public IEnumerable<Product> GetAll()
        {
            var repository = _repositoryFactory.Repository<Product>();
            var query = repository.MultipleResultQuery();

            return repository.Search(query);
        }

        public void Update(Product model)
        {
            var repository = _unitOfWork.Repository<Product>();

            repository.Update(model);
            _unitOfWork.SaveChanges();
        }

        public void Delete(long id)
        {
            var repository = _unitOfWork.Repository<Product>();

            if (!repository.Any(product => product.Id == id))
                throw new ArgumentNullException($"Produto não encontrado para o id: {id}");

            repository.Remove(product => product.Id == id);
        }
    }
}
