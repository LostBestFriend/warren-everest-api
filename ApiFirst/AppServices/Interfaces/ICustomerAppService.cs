using AppModels.AppModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Interfaces
{
    public interface ICustomerAppService
    {
        Task<long> CreateAsync(CreateCustomerDTO model);
        void Update(long id, UpdateCustomerDTO model);
        void DeleteAsync(long id);
        IEnumerable<CustomerResponseDTO> GetAll();
        Task<CustomerResponseDTO> GetByIdAsync(long id);
        void Modify(long id, UpdateCustomerDTO model);
        Task<CustomerResponseDTO> GetByCpfAsync(string cpf);
    }
}
