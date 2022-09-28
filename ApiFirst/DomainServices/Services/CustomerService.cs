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
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repositoryFactory = repository ?? (IRepositoryFactory)_unitOfWork;
        }

        public async Task<long> CreateAsync(Customer model)
        {
            var repo = _unitOfWork.Repository<Customer>();

            if (repo.Any(customer => customer.Email == model.Email)) throw new ArgumentException("Email já está sendo usado.");
            if (repo.Any(customer => customer.Cpf == model.Cpf)) throw new ArgumentException("O CPF já está sendo usado.");

            await repo.AddAsync(model).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return model.Id;
        }

        public async void DeleteAsync(long id)
        {
            var repository = _unitOfWork.Repository<Customer>();

            Customer customer = await GetByIdAsync(id);

            repository.Remove(customer);
        }

        public IEnumerable<Customer> GetAll()
        {
            var repo = _repositoryFactory.Repository<Customer>();
            var query = repo.MultipleResultQuery();

            return repo.Search(query);
        }

        public async Task<Customer> GetByCpfAsync(string cpf)
        {
            var repo = _repositoryFactory.Repository<Customer>();
            var query = repo.SingleResultQuery().AndFilter(customer => customer.Cpf == cpf);
            var response = await repo.FirstOrDefaultAsync(query).ConfigureAwait(false);

            if (response is null) throw new ArgumentNullException($"Não foi encontrado Customer para o CPF: {cpf}");
            return response;
        }

        public void Update(Customer model)
        {
            var repo = _unitOfWork.Repository<Customer>();
            if (!repo.Any(customer => customer.Id == model.Id)) throw new ArgumentNullException($"Não foi encontrado Customer para o Id: {model.Id}");

            if (repo.Any(customer => customer.Email == model.Email && customer.Id != model.Id)) throw new ArgumentException($"Já existe usuário com o Email {model.Email}");
            if (repo.Any(customer => customer.Cpf == model.Cpf && customer.Id != model.Id)) throw new ArgumentException($"Já existe usuário com o CPF {model.Cpf}");

            repo.Update(model);
            _unitOfWork.SaveChanges();
        }

        public async Task<Customer> GetByIdAsync(long id)
        {
            var repo = _repositoryFactory.Repository<Customer>();
            var query = repo.SingleResultQuery().AndFilter(customer => customer.Id == id);
            var response = await repo.FirstOrDefaultAsync(query).ConfigureAwait(false);

            if (response is null) throw new ArgumentNullException($"Não foi encontrado Customer para o Id: {id}");
            return response;
        }

        public void Modify(Customer model)
        {
            var repo = _unitOfWork.Repository<Customer>();

            if (!repo.Any(customer => customer.Id == model.Id)) throw new ArgumentNullException($"Não foi encontrado Customer para o Id: {model.Id}");

            if (repo.Any(customer => customer.Cpf == model.Cpf && customer.Id != model.Id)) throw new ArgumentException($"Já existe usuário com o CPF {model.Cpf}");
            if (repo.Any(customer => customer.Email == model.Email && customer.Id != model.Id)) throw new ArgumentException($"Já existe usuário com o CPF {model.Email}");

            repo.Update(model);
            _unitOfWork.SaveChanges();
        }
    }
}
