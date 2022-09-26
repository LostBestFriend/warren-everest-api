using AppModels.AppModels;

namespace AppServices.Interfaces
{
    public interface ICustomerAppService
    {
        Task<long> CreateAsync(CreateCustomerDTO model);
        void Update(int id, UpdateCustomerDTO model);
        void Delete(int id);
        IEnumerable<CustomerResponseDTO> GetAll();
        Task<CustomerResponseDTO> GetByIdAsync(int id);
        void Modify(int id, UpdateCustomerDTO model);
        Task<CustomerResponseDTO> GetByCpfAsync(string cpf);
    }
}
