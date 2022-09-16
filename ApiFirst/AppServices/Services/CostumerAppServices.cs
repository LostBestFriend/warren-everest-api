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
            return _customerServices.GetByCpf(cpf);
        }

        public bool Create(Customer model)
        {
            return _customerServices.Create(model);
        }

        public bool Delete(int id)
        {
            return _customerServices.Delete(id);
        }

        public List<Customer> GetAll()
        {
            return _customerServices.GetAll();
        }

        public Customer? GetById(int id)
        {
            return _customerServices.GetById(id);
        }
        public int Update(int id, Customer model)
        {
            return _customerServices.Update(id, model);
        }
        public int Modify(int id, Customer model)
        {
            return _customerServices.Modify(id, model);
        }
    }
}

