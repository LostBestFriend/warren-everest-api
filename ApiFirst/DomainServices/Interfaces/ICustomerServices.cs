using DomainModels.Models;

namespace DomainServices.Interfaces
{
    public interface ICustomerServices
    {
        Customer Create(Customer model);

        void Update(int id, Customer model);

        bool Delete(int id);

        List<Customer> GetAll();

        Customer? GetById(int id);

        void Modify(int id, string email);
        Customer? GetByCpf(string cpf);
    }
}
