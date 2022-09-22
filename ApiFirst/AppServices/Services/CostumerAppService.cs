using AppServices.Interfaces;
using DomainModels.Models;
using DomainServices.Interfaces;

namespace AppServices.Services
{
    public class CustomerAppServices : ICustomerAppService
    {
        private readonly ICustomerService _customerServices;

        public CustomerAppServices(ICustomerService customerServices)
        {
            _customerServices = customerServices;
        }

        public Customer? GetByCpf(string cpf)
        {
            var response = _customerServices.GetByCpf(cpf);
            return response;
        }

        public long Create(Customer model)
        {
            var response = _customerServices.Create(model);
            return response;
        }

        public void Delete(int id)
        {
            _customerServices.Delete(id);
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
        public void Update(int id, Customer model)
        {
            _customerServices.Update(id, model);
        }
        public void Modify(int id, string email)
        {
            _customerServices.Modify(id, email);
        }
    }
}

