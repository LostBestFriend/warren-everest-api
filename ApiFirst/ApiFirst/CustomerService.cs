﻿using DomainModels.Models;
using DomainServices.Interfaces;

namespace DomainServices.Repositories
{
    public class CustomerService : ICustomerService
    {
        private readonly List<Customer> _customers = new();

        public bool Create(Customer model)
        {
            model.Id = _customers.LastOrDefault()?.Id + 1 ?? 1;

            if (Exists(model))
            {
                return false;
            }
            else
            {
                _customers.Add(model);
                return true;
            }
        }


        public bool Delete(int id)
        {
            int index = _customers.FindIndex(customer => customer.Id == id);
            if (index == -1) return false;
            _customers.RemoveAt(index);
            return true;
        }

        public List<Customer> GetAll()
        {
            return _customers;
        }

        public Customer? GetByCpf(string cpf)
        {
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            return _customers.FirstOrDefault(customer => customer.Cpf == cpf);
        }

        public bool ExistsUpdate(long id, Customer model)
        {
            return _customers.Any(customer => (customer.Cpf == model.Cpf || customer.Email == model.Email) && customer.Id != id);
        }

        public bool Exists(Customer model)
        {
            return _customers.Any(customer => customer.Cpf == model.Cpf || customer.Email == model.Email);
        }

        public int Update(int id, Customer model)
        {

            int index = _customers.FindIndex(customer => customer.Id == id);
            if (index == -1) return -1;

            if (ExistsUpdate(id, model)) return 0;
            else
            {
                model.Id = _customers[index].Id;
                _customers[index] = model;
                return 1;
            }
        }

        public Customer? GetById(int id)
        {
            return _customers.FirstOrDefault(x => x.Id == id);
        }

        public int Modify(int id, Customer model)
        {
            int index = _customers.FindIndex(customer => customer.Id == id);
            if (index == -1) return -1;

            if (ExistsUpdate(id, model)) return 0;
            else
            {
                model.Id = _customers[index].Id;

                if (_customers[index].FullName != model.FullName) _customers[index].FullName = model.FullName;

                if (_customers[index].Email != model.Email) _customers[index].Email = model.Email;

                if (_customers[index].EmailConfirmation != model.EmailConfirmation) _customers[index].EmailConfirmation = model.EmailConfirmation;

                if (_customers[index].Cpf != model.Cpf) _customers[index].Cpf = model.Cpf;

                if (_customers[index].Cellphone != model.Cellphone) _customers[index].Cellphone = model.Cellphone;

                if (_customers[index].DateOfBirth != model.DateOfBirth) _customers[index].DateOfBirth = model.DateOfBirth;

                if (_customers[index].EmailSms != model.EmailSms) _customers[index].EmailSms = model.EmailSms;

                if (_customers[index].Whatsapp != model.Whatsapp) _customers[index].Whatsapp = model.Whatsapp;

                if (_customers[index].Country != model.Country) _customers[index].Country = model.Country;

                if (_customers[index].City != model.City) _customers[index].City = model.City;

                if (_customers[index].PostalCode != model.PostalCode) _customers[index].PostalCode = model.PostalCode;

                if (_customers[index].Address != model.Address) _customers[index].Address = model.Address;

                if (_customers[index].Number != model.Number) _customers[index].Number = model.Number;

                return 1;
            }
        }
    }
}
