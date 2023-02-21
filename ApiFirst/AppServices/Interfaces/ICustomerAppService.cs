using AppModels.AppModels.Customers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Interfaces
{
    public interface ICustomerAppService
    {
        Task<long> CreateAsync(CreateCustomer model);
        void Update(UpdateCustomer model);
        Task DeleteAsync(long id);
        Task<IEnumerable<CustomerResponse>> GetAllAsync();
        Task<CustomerResponse> GetByIdAsync(long id);
        Task<CustomerResponse> GetByCpfAsync(string cpf);
    }
}
