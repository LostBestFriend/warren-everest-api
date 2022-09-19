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

        public bool Create(Customer model)
        {
            model.Id = _customers.LastOrDefault()?.Id + 1 ?? 0;

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
            var response = _customers.FirstOrDefault(customer => customer.Id == id);

            if (response is null) return false;

            int index = _customers.IndexOf(response);

            if (index == -1) return false;

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

        public int Update(int id, Customer model)
        {

            var response = _customers.FirstOrDefault(customer => customer.Id == id);

            if (response is null) return -1;

            int index = _customers.IndexOf(response);

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
            var response = _customers.FirstOrDefault(customer => customer.Id == id);
            return response;
        }

        public int Modify(int id, string email)
        {
            Customer? model = _customers.FirstOrDefault(customer => customer.Id == id);
            if (model is null) return -1;

            else if (_customers.Any(customer => customer.Email == model.Email)) return 0;
            else
            {
                model.Id = id;
                model.Email = email;
                return 1;
            }
        }
    }
}
