using DomainModels.Models;

namespace DomainServices.Interfaces
{
    public interface ICustomerService
    {
        bool Create(Customer model);

        int Update(int id, Customer model);

        bool Delete(int id);

        List<Customer> GetAll();

        Customer? GetById(int id);

        int Modify(int id, Customer model);

        Customer? GetByCpf(string cpf);
    }
}
