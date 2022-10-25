using DomainModels.Models;
using DomainServices.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork<WarrenContext> unitOfWork, IRepositoryFactory<WarrenContext> repository)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            _repositoryFactory = repository ?? (IRepositoryFactory)_unitOfWork;
        }

        public async Task<long> CreateAsync(Customer model)
        {
            var repository = _unitOfWork.Repository<Customer>();

            if (repository.Any(customer => customer.Email == model.Email))
                throw new ArgumentException("Email já está sendo usado.");
            if (repository.Any(customer => customer.Cpf == model.Cpf))
                throw new ArgumentException("O CPF já está sendo usado.");

            await repository.AddAsync(model).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return model.Id;
        }

        public async void Delete(long id)
        {
            var repository = _unitOfWork.Repository<Customer>();
            var response = await GetByIdAsync(id);

            repository.Remove(response);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<Customer> GetAll()
        {
            var repository = _repositoryFactory.Repository<Customer>();
            var query = repository.MultipleResultQuery();

            return repository.Search(query);
        }

        public async Task<Customer> GetByCpfAsync(string cpf)
        {
            var repository = _repositoryFactory.Repository<Customer>();
            var query = repository.SingleResultQuery().AndFilter(customer => customer.Cpf == cpf);
            var response = await repository.FirstOrDefaultAsync(query).ConfigureAwait(false);

            if (response is null)
                throw new ArgumentNullException($"Não foi encontrado Customer para o CPF: {cpf}");
            return response;
        }

        public void Update(Customer model)
        {
            var repository = _unitOfWork.Repository<Customer>();
            if (!repository.Any(customer => customer.Id == model.Id))
                throw new ArgumentNullException($"Não foi encontrado Customer para o Id: {model.Id}");
            if (repository.Any(customer => customer.Email == model.Email && customer.Id != model.Id))
                throw new ArgumentException($"Já existe usuário com o Email {model.Email}");
            if (repository.Any(customer => customer.Cpf == model.Cpf && customer.Id != model.Id))
                throw new ArgumentException($"Já existe usuário com o CPF {model.Cpf}");

            repository.Update(model);
            _unitOfWork.SaveChanges();
        }

        public async Task<Customer> GetByIdAsync(long id)
        {
            var repository = _repositoryFactory.Repository<Customer>();
            var query = repository.SingleResultQuery().AndFilter(customer => customer.Id == id);
            var response = await repository.FirstOrDefaultAsync(query).ConfigureAwait(false);

            if (response is null)
                throw new ArgumentNullException($"Não foi encontrado Customer para o Id: {id}");
            return response;
        }
    }
}
