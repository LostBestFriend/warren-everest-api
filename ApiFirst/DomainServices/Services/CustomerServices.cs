using DomainModels.Models;
using DomainServices.Interfaces;

namespace DomainServices.Repositories
{
    public class CustomerServices : ICustomerServices
    {
        private readonly List<Customer> _customers = new();

        public bool Create(Customer model)
        {
            model.Id = _customers.LastOrDefault()?.Id + 1 ?? 0;

            if (Exists(model))
            {
                return false;
            }
            _customers.Add(model);
            return true;
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

        public bool Delete(int id)
        {
            int index = _customers.FindIndex(customer => customer.Id == id);

            if (index == -1) return false;

            return true;
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

        public int Update(int id, Customer model)
        {

            int index = _customers.FindIndex(customer => customer.Id == id);

            if (index == -1) return -1;

            if (ExistsUpdate(id, model)) return 0;

            model.Id = _customers[index].Id;
            _customers[index] = model;
            return 1;
        }

        public Customer? GetById(int id)
        {
            var response = _customers.FirstOrDefault(customer => customer.Id == id);
            return response;
        }

        public int Modify(int id, string email)
        {
            int index = _customers.FindIndex(customer => customer.Id == id);

            if (index == -1) return -1;

            else if (_customers.Any(customer => customer.Email == email)) return 0;

            _customers[index].Id = id;
            _customers[index].Email = email;
            return 1;
        }
    }
}
