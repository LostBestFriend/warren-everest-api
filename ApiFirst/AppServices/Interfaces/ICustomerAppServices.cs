using AppModels.MapperModels;

namespace AppServices.Interfaces
{
    public interface ICustomerAppServices
    {
        long Create(CustomerCreateDTO model);

        void Update(int id, CustomerUpdateDTO model);

        void Delete(int id);

        List<CustomerResponseDTO> GetAll();

        CustomerResponseDTO? GetById(int id);

        void Modify(int id, string email);

        CustomerResponseDTO? GetByCpf(string cpf);
    }
}
