using AppServices.Interfaces;
using DomainModels.Models;
using DomainServices.Interfaces;

namespace AppServices.Services
{
    public class CustomerAppServices : ICustomerAppServices
    {
        private readonly ICustomerServices _customerServices;

        public CustomerAppServices(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }

        public Customer? GetByCpf(string cpf)
        {
            var response = _customerServices.GetByCpf(cpf);
            return response;
        }

        public bool Create(Customer model)
        {
            var response = _customerServices.Create(model);
            return response;
        }

        public bool Delete(int id)
        {
            var response = _customerServices.Delete(id);
            return response;
        }

        public List<Customer> GetAll()
        {
            var response = _customerServices.GetAll();
            return response;
        }

        public Customer? GetById(int id)
        {
            var response = _customerServices.GetById(id);
            return response;
        }
        public int Update(int id, Customer model)
        {
            var response = _customerServices.Update(id, model);
            return response;
        }
        public int Modify(int id, string email)
        {
            var response = _customerServices.Modify(id, email);
            return response;
        }
    }
}

