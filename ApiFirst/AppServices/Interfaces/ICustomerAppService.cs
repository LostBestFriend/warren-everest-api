using AppModels.AppModels;

namespace AppServices.Interfaces
{
    public interface ICustomerAppService
    {
        long Create(CreateCustomerDTO model);
        void Update(int id, UpdateCustomerDTO model);
        void Delete(int id);
        List<CustomerResponseDTO> GetAll();
        CustomerResponseDTO GetById(int id);
        void ChangeEmail(int id, string email);
        CustomerResponseDTO GetByCpf(string cpf);
    }
}
