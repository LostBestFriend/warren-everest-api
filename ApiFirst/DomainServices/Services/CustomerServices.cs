using DomainModels.Models;
using DomainServices.Interfaces;

namespace DomainServices.Repositories
{
    public class CustomerServices : ICustomerServices
    {
        private readonly IList<Customer> _customers;

        public CustomerServices()
        {
            _customers = new List<Customer>();
        }

        public Customer Create(Customer model)
        {
            model.Id = _customers.LastOrDefault()?.Id + 1 ?? 0;
            if (Exists(model))
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
            var response = _customers.FirstOrDefault(customer => customer.Id == id);

            if (response is null) return false;

            int index = _customers.IndexOf(response);

            if (index == -1) return false;
            _customers.RemoveAt(index);
            return true;
        }

        public IList<Customer> GetAll()
        {
            return _customers;
        }

        public Customer? GetByCpf(string cpf)
        {
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            var response = _customers.FirstOrDefault(customer => customer.Cpf == cpf);
            return response;
        }

        public void Update(int id, Customer model)
        {
            var response = _customers.FirstOrDefault(customer => customer.Id == id);

            if (response is null) throw new ArgumentException($"$Não foi encontrado Customer para o Id: {model.Id}");

            int index = _customers.IndexOf(response);

            if (ExistsUpdate(_customers[index].Id, model)) throw new ArgumentException($"Já existe usuário com o E-mail ou CPF digitados");
            else
            {
                _customers[index] = model;
            }
        }

        public bool ExistsUpdate(long id, Customer model)
        {
            var response = _customers.Any(customer => (customer.Cpf == model.Cpf || customer.Email == model.Email) && customer.Id != id);
            return response;
        }

        public bool Exists(Customer model)
        {
            var response = _customers.Any(customer => customer.Cpf == model.Cpf || customer.Email == model.Email);
            return response;
        }

        public Customer? GetById(int id)
        {
            return _customers.FirstOrDefault(x => x.Id == id);
        }

        public void Modify(int id, string email)
        {
            Customer? response = _customers.FirstOrDefault(customer => customer.Id == id);
            if (response is null) throw new ArgumentException($"$Não foi encontrado Customer para o Id: {id}");

            else if (_customers.Any(customer => customer.Email == response.Email)) throw new ArgumentException($"Já existe usuário com o E-mail digitado");
            else
            {
                response.Id = id;
                response.Email = email;
            }
        }
    }
}
