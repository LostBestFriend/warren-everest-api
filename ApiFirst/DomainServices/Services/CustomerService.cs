using DomainModels.Models;
using DomainServices.Interfaces;

namespace DomainServices.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly List<Customer> _customers = new();

        public Customer Create(Customer model)
        {
            model.Id = _customers.LastOrDefault()?.Id + 1 ?? 0;

            if (_customers.Any(customer => customer.Cpf == model.Cpf)) throw new ArgumentException($"Já existe usuário com o CPF {model.Cpf}");
            if (_customers.Any(customer => customer.Email == model.Email)) throw new ArgumentException($"Já existe usuário com o CPF {model.Email}");

            _customers.Add(model);
            return model;
        }

        public void Delete(int id)
        {
            int index = _customers.FindIndex(customer => customer.Id == id);

            if (index == -1) throw new ArgumentNullException($"Cliente não encontrado para o id: {id}");

            _customers.RemoveAt(index);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _customers;
        }

        public Customer GetByCpf(string cpf)
        {
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            var response = _customers.FirstOrDefault(customer => customer.Cpf == cpf);
            if (response is null) throw new ArgumentNullException($"$Não foi encontrado Customer para o CPF: {cpf}");
            return response;
        }

        public void Update(int id, Customer model)
        {
            int index = _customers.FindIndex(customer => customer.Id == id);

            if (index == -1) throw new ArgumentException($"$Não foi encontrado Customer para o Id: {id}");

            if (_customers.Any(customer => customer.Cpf == model.Cpf)) throw new ArgumentException($"Já existe usuário com o CPF {model.Cpf}");
            if (_customers.Any(customer => customer.Email == model.Email)) throw new ArgumentException($"Já existe usuário com o Email {model.Email}");

            _customers[index] = model;
        }

        public Customer GetById(int id)
        {
            var response = _customers.FirstOrDefault(x => x.Id == id);
            if (response is null) throw new ArgumentNullException($"$Não foi encontrado Customer para o Id: {id}");
            return response;
        }

        public void UpdateEmail(int id, string email)
        {
            int index = _customers.FindIndex(customer => customer.Id == id);

            if (index == -1) throw new ArgumentException($"$Não foi encontrado Customer para o Id: {id}");

            if (_customers.Any(customer => customer.Email == email)) throw new ArgumentException($"Já existe usuário com o E-mail digitado");

            _customers[index].Id = id;
            _customers[index].Email = email;
        }
    }
}
