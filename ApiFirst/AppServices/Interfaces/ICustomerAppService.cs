using AppModels.MapperModels;

namespace AppServices.Interfaces
{
    public interface ICustomerAppServices
    {
        Task<long> CreateAsync(CustomerCreateDTO model);

        void Update(int id, CustomerUpdateDTO model);

        void Delete(int id);

        IEnumerable<CustomerResponseDTO> GetAll();

        Task<CustomerResponseDTO>? GetByIdAsync(int id);

        void Modify(int id, CustomerUpdateDTO model);

        Task<CustomerResponseDTO>? GetByCpfAsync(string cpf);
    }
}
