using AppModels.AppModels;
using System.Collections.Generic;

namespace AppServices.Interfaces
{
    public interface ICustomerAppService
    {
        long Create(CreateCustomerDTO model);
        void Update(UpdateCustomerDTO model);
        void Delete(long id);
        IEnumerable<CustomerResponseDTO> GetAll();
        CustomerResponseDTO GetById(long id);
        void Modify(UpdateCustomerDTO model);
        CustomerResponseDTO GetByCpf(string cpf);
    }
}
