using DomainModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Interfaces
{
    public interface ICustomerService
    {
        Task<long> CreateAsync(Customer model);
        void Update(Customer model);
        void Delete(long id);
        IEnumerable<Customer> GetAll();
        Task<Customer> GetByIdAsync(long id);
        Task<Customer> GetByCpfAsync(string cpf);
    }
}
