using DomainModels.Models;
using DomainServices.Interfaces;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainServices.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly WarrenContext _context;
        private readonly DbSet<Customer> _customers;

        public CustomerService(WarrenContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _customers = _context.Set<Customer>();
        }

        public long Create(Customer model)
        {
            if (_customers.Any(customer => customer.Cpf == model.Cpf)) throw new ArgumentException($"Já existe usuário com o CPF {model.Cpf}");
            if (_customers.Any(customer => customer.Email == model.Email)) throw new ArgumentException($"Já existe usuário com o CPF {model.Email}");

            _customers.Add(model);
            _context.SaveChanges();
            return model.Id;
        }

        public void Delete(long id)
        {
            Customer customer = GetById(id);

            _customers.Remove(customer).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
        }

        public IEnumerable<Customer> GetAll()
        {
            return _customers;
        }

        public Customer GetByCpf(string cpf)
        {
            var response = _customers.FirstOrDefault(customer => customer.Cpf == cpf);
            if (response is null) throw new ArgumentNullException($"Não foi encontrado Customer para o CPF: {cpf}");
            return response;
        }

        public void Update(Customer model)
        {
            if (!_customers.Any(customer => customer.Id == model.Id)) throw new ArgumentNullException($"Não foi encontrado Customer para o Id: {model.Id}");
            if (_customers.Any(customer => customer.Cpf == model.Cpf && customer.Id != model.Id)) throw new ArgumentException($"Já existe usuário com o CPF {model.Cpf}");
            if (_customers.Any(customer => customer.Email == model.Email && customer.Id != model.Id)) throw new ArgumentException($"Já existe usuário com o Email {model.Email}");

            _customers.Update(model);
            _context.SaveChanges();
        }

        public Customer GetById(long id)
        {
            var response = _customers.FirstOrDefault(customer => customer.Id == id);

            if (response is null) throw new ArgumentNullException($"Não foi encontrado Customer para o Id: {id}");
            return response;
        }
    }
}
