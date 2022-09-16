using DomainModels.Models;
using DomainServices.Interfaces;

namespace DomainServices.Repositories
{
    public class CustomerServices : ICustomerServices
    {
        private readonly List<Customer> _customers = new();

        public Customer Create(Customer model)
        {
            model.Id = _customers.LastOrDefault()?.Id + 1 ?? 0;

            bool exist = _customers.Any(customer => customer.Cpf == model.Cpf || customer.Email == model.Email);
            if (exist)
            {
                throw new ArgumentException("O CPF ou Email já estão sendo usados.");
            }
            else
            {
                _customers.Add(model);
                return model;
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

        public void Update(Customer model)
        {

            int index = _customers.FindIndex(customer => customer.Id == model.Id);
            if (index == -1) throw new ArgumentException($"$Não foi encontrado Customer para o Id: {model.Id}");

            bool exist = _customers.Any(customer => (customer.Cpf == model.Cpf || customer.Email == model.Email) && customer.Id != _customers[index].Id);

            if (exist) throw new ArgumentException($"Já existe usuário com o E-mail ou CPF digitados");
            else
            {
                _customers[index] = model;
            }
        }

        public Customer? GetById(int id)
        {
            return _customers.FirstOrDefault(x => x.Id == id);
        }

        public void Modify(Customer model)
        {
            int index = _customers.FindIndex(customer => customer.Id == model.Id);
            if (index == -1) throw new ArgumentException($"$Não foi encontrado Customer para o Id: {model.Id}");

            bool exist = _customers.Any(customer => (customer.Cpf == model.Cpf || customer.Email == model.Email) && customer.Id != _customers[index].Id);
            if (exist) throw new ArgumentException($"Já existe usuário com o E-mail ou CPF digitados");
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
            }
        }
    }
}
