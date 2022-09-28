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
        void UpdateEmail(long id, string email);
        CustomerResponseDTO GetByCpf(string cpf);
    }
}
