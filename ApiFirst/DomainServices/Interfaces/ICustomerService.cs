using DomainModels.Models;

namespace DomainServices.Interfaces
{
    public interface ICustomerService
    {
        Customer Create(Customer model);
        void Update(int id, Customer model);
        void Delete(int id);
        List<Customer> GetAll();
        Customer GetById(int id);
        void ChangeEmail(int id, string email);
        Customer GetByCpf(string cpf);
    }
}
