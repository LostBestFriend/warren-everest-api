using DomainModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Interfaces
{
    public interface ICustomerService
    {
        Task<long> CreateAsync(Customer model);
        void Update(Customer model);
        Task DeleteAsync(long id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(long id);
        Task<Customer> GetByCpfAsync(string cpf);
    }
}
