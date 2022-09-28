using AppModels.AppModels;
using System.Collections.Generic;

namespace AppServices.Interfaces
{
    public interface ICustomerAppService
    {
        long Create(CreateCustomerDTO model);
        void Update(long id, UpdateCustomerDTO model);
        void Delete(long id);
        IEnumerable<CustomerResponseDTO> GetAll();
        CustomerResponseDTO GetById(long id);
        void Modify(long id, UpdateCustomerDTO model);
        CustomerResponseDTO GetByCpf(string cpf);
    }
}
