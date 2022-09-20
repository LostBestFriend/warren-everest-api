using DomainModels.Models;

namespace AppServices.Interfaces
{
    public interface ICustomerAppServices
    {
        bool Create(Customer model);

        int Update(int id, Customer model);

        bool Delete(int id);

        List<Customer> GetAll();

        Customer? GetById(int id);

        int Modify(int id, string email);

        Customer? GetByCpf(string cpf);
    }
}
