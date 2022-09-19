namespace ApiFirst
{
    public interface ICustomerService
    {
        bool Create(Customer model);

        int Update(int id, Customer model);

        bool Delete(int id);

        IList<Customer> GetAll();

        Customer? GetById(int id);

        int Modify(int id, string email);

        Customer? GetByCpf(string cpf);
    }
}
