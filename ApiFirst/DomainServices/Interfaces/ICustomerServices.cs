using DomainModels.Models;

namespace DomainServices.Interfaces
{
    public interface ICustomerServices
    {
        Customer Create(Customer model);

        void Update(Customer model);

        bool Delete(int id);

        List<Customer> GetAll();

        Customer? GetById(int id);

        void Modify(int id, Customer model);

        Customer? GetByCpf(string cpf);
    }
}
