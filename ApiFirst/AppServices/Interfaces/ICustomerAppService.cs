using AppModels.AppModels;

namespace AppServices.Interfaces
{
    public interface ICustomerAppService
    {
        long Create(CreateCustomerDTO model);
        void Update(int id, UpdateCustomerDTO model);
        void Delete(int id);
        IEnumerable<CustomerResponseDTO> GetAll();
        CustomerResponseDTO GetById(int id);
        void UpdateEmail(int id, string email);
        CustomerResponseDTO GetByCpf(string cpf);
    }
}
