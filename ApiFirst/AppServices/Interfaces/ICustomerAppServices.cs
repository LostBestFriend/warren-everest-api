using AppModels.MapperModels;

namespace AppServices.Interfaces
{
    public interface ICustomerAppServices
    {
        long Create(CustomerCreateDTO model);

        void Update(int id, CustomerUpdateDTO model);

        bool Delete(int id);

        List<CustomerResponseDTO> GetAll();

        CustomerResponseDTO? GetById(int id);

        void Modify(int id, CustomerUpdateDTO model);

        CustomerResponseDTO? GetByCpf(string cpf);
    }
}
