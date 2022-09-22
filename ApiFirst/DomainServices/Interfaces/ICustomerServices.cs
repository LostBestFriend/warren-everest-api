using DomainModels.Models;

namespace DomainServices.Interfaces
{
    public interface ICustomerServices
    {
        Task<long> CreateAsync(Customer model);

        void Update(Customer model);

        void Delete(int id);

        IEnumerable<Customer> GetAll();

        Task<Customer>? GetByIdAsync(int id);

        void Modify(Customer model);

        Task<Customer>? GetByCpfAsync(string cpf);
    }
}
