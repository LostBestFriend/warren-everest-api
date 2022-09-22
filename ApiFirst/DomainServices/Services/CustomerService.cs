﻿using DomainModels.Models;
using DomainServices.Interfaces;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

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
            model.Id = _customers.ToList().LastOrDefault()?.Id + 1 ?? 1;
            if (_customers.Any(customer => customer.Cpf == model.Cpf)) throw new ArgumentException($"Já existe usuário com o CPF {model.Cpf}");
            if (_customers.Any(customer => customer.Email == model.Email)) throw new ArgumentException($"Já existe usuário com o CPF {model.Email}");

            _customers.Add(model);
            _context.SaveChanges();
            return model.Id;
        }

        public void Delete(int id)
        {
            Customer? customer = _customers.FirstOrDefault(customer => customer.Id == id);

            if (customer is null) throw new ArgumentNullException($"Cliente não encontrado para o id: {id}");

            _customers.Remove(customer).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
        }

        public List<Customer> GetAll()
        {
            return _customers.ToList();
        }

        public Customer GetByCpf(string cpf)
        {
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            var response = _customers.FirstOrDefault(customer => customer.Cpf == cpf);
            if (response is null) throw new ArgumentNullException($"$Não foi encontrado Customer para o CPF: {cpf}");
            return response;
        }

        public void Update(Customer model)
        {

            int index = _customers.ToList().FindIndex(customer => customer.Id == model.Id);
            if (index == -1) throw new ArgumentNullException($"$Não foi encontrado Customer para o Id: {model.Id}");
            if (_customers.Any(customer => customer.Cpf == model.Cpf)) throw new ArgumentException($"Já existe usuário com o CPF {model.Cpf}");
            if (_customers.Any(customer => customer.Email == model.Email)) throw new ArgumentException($"Já existe usuário com o CPF {model.Email}");

            _customers.Update(model);
            _context.SaveChanges();
        }

        public Customer GetById(int id)
        {
            var response = _customers.FirstOrDefault(x => x.Id == id);
            if (response is null) throw new ArgumentNullException($"$Não foi encontrado Customer para o Id: {id}");
            return response;
        }

        public void Modify(Customer model)
        {
            int index = _customers.ToList().FindIndex(customer => customer.Id == model.Id);
            if (index == -1) throw new ArgumentNullException($"Não foi encontrado Customer para o Id: {model.Id}");

            if (_customers.Any(customer => customer.Cpf == model.Cpf)) throw new ArgumentException($"Já existe usuário com o CPF {model.Cpf}");
            if (_customers.Any(customer => customer.Email == model.Email)) throw new ArgumentException($"Já existe usuário com o CPF {model.Email}");

            model.Id = _customers.ToList()[index].Id;
            _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
