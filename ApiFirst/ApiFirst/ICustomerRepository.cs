using DomainModels.Models;

namespace DomainServices.Interfaces
{
    public interface ICustomerRepository
    {
        bool Create(Customer model);

        int Update(string cpf, Customer model);

        bool Delete(int id);

        List<Customer> GetAll();

        Customer GetById(int id);

        int Modify(string cpf, Customer model);

        Customer? GetByCpf(string cpf);
    }
}
