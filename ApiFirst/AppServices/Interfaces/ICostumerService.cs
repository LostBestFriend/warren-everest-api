using AppServices.DTOs;

namespace AppServices.Interfaces
{
    public interface ICustomerService : IService<CustomerDTO>
    {
        public int DeleteFromList(string cpf, string email);
        public CustomerDTO GetSpecificFromList(string cpf, string email);
    }
}
