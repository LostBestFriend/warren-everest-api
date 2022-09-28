using DomainModels.Models;
using System.Collections.Generic;

namespace DomainServices.Interfaces
{
    public interface ICustomerService
    {
        long Create(Customer model);
        void Update(Customer model);
        void Delete(long id);
        IEnumerable<Customer> GetAll();
        Customer GetById(long id);
        void Modify(Customer model);
        Customer GetByCpf(string cpf);
    }
}
