using DomainModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Interfaces
{
    public interface ICustomerService
    {
        Task<long> CreateAsync(Customer model);
        void Update(Customer model);
        void DeleteAsync(long id);
        IEnumerable<Customer> GetAll();
        Task<Customer> GetByIdAsync(long id);
        void Modify(Customer model);
        Task<Customer> GetByCpfAsync(string cpf);
    }
}
