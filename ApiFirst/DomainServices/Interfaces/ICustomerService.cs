using DomainModels.Models;

namespace DomainServices.Interfaces
{
    public interface ICustomerService
    {
        long Create(Customer model);

        void Update(Customer model);

        void Delete(int id);

        List<Customer> GetAll();

        Customer? GetById(int id);

        void Modify(Customer model);

        Customer? GetByCpf(string cpf);
    }
}
