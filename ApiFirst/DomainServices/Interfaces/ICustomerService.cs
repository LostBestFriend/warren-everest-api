using DomainModels.Models;
using System.Collections.Generic;

namespace DomainServices.Interfaces
{
    public interface ICustomerService
    {
        Customer Create(Customer model);
        void Update(Customer model);
        void Delete(long id);
        IEnumerable<Customer> GetAll();
        Customer GetById(long id);
        void UpdateEmail(long id, string email);
        Customer GetByCpf(string cpf);
    }
}
