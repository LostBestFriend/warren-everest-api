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
            if (Exists(model)) throw new ArgumentException("O CPF ou Email já estão sendo usados.");

            _customers.Add(model);
            return model;
        }

        public void Delete(int id)
        {
            int index = _customers.FindIndex(customer => customer.Id == id);

            if (index == -1) throw new ArgumentNullException($"Cliente não encontrado para o id: {id}");

            _customers.RemoveAt(index);
        }

        public List<Customer> GetAll()
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
            int index = _customers.FindIndex(customer => customer.Id == id);

            if (index == -1) throw new ArgumentException($"$Não foi encontrado Customer para o Id: {id}");

            if (ExistsUpdate(_customers[index].Id, model)) throw new ArgumentException($"Já existe usuário com o E-mail ou CPF digitados");

            _customers[index] = model;
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
            var response = _customers.FirstOrDefault(x => x.Id == id);
            return response;
        }

        public void Modify(int id, string email)
        {
            int index = _customers.FindIndex(customer => customer.Id == id);

            if (index == -1) throw new ArgumentException($"$Não foi encontrado Customer para o Id: {id}");

            if (_customers.Any(customer => customer.Email == email)) throw new ArgumentException($"Já existe usuário com o E-mail digitado");

            _customers[index].Id = id;
            _customers[index].Email = email;
        }
    }
}
