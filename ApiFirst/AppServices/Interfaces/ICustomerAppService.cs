using AppModels.AppModels.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Interfaces
{
    public interface ICustomerAppService
    {
        Task<long> CreateAsync(CreateCustomer model);
        void Update(UpdateCustomer model);
        void Delete(long id);
        IEnumerable<CustomerResponse> GetAll();
        Task<CustomerResponse> GetByIdAsync(long id);
        Task<CustomerResponse> GetByCpfAsync(string cpf);
    }
}
