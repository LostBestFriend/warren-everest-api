using DomainModels.Models;
using DomainServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainServices.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly List<Customer> _customers = new();

        public Customer Create(Customer model)
        {
            model.Id = _customers.LastOrDefault()?.Id + 1 ?? 1;

            if (_customers.Any(customer => customer.Cpf == model.Cpf)) throw new ArgumentException($"Já existe usuário com o CPF {model.Cpf}");
            if (_customers.Any(customer => customer.Email == model.Email)) throw new ArgumentException($"Já existe usuário com o CPF {model.Email}");

            _customers.Add(model);
            return model;
        }

        public void Delete(long id)
        {
            Customer customer = GetById(id);

            _customers.Remove(customer);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _customers;
        }

        public Customer GetByCpf(string cpf)
        {
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            var response = _customers.FirstOrDefault(customer => customer.Cpf == cpf);
            if (response is null) throw new ArgumentNullException($"Não foi encontrado Customer para o CPF: {cpf}");
            return response;
        }

        public void Update(Customer model)
        {
            int index = _customers.FindIndex(customer => customer.Id == model.Id);

            if (index == -1) throw new ArgumentNullException($"Não foi encontrado Customer para o Id: {model.Id}");

            if (_customers.Any(customer => customer.Email == model.Email && customer.Id != model.Id)) throw new ArgumentException($"Já existe usuário com o Email {model.Email}");
            if (_customers.Any(customer => customer.Cpf == model.Cpf && customer.Id != model.Id)) throw new ArgumentException($"Já existe usuário com o CPF {model.Cpf}");

            _customers[index] = model;
        }

        public Customer GetById(long id)
        {
            var response = _customers.FirstOrDefault(customer => customer.Id == id);
            if (response is null) throw new ArgumentNullException($"Não foi encontrado Customer para o Id: {id}");
            return response;
        }

        public void UpdateEmail(long id, string email)
        {
            int index = _customers.FindIndex(customer => customer.Id == id);

            if (index == -1) throw new ArgumentNullException($"Não foi encontrado Customer para o Id: {id}");

            if (_customers.Any(customer => customer.Email == email)) throw new ArgumentException($"Já existe usuário com o E-mail digitado");

            _customers[index].Id = id;
            _customers[index].Email = email;
        }
    }
}
