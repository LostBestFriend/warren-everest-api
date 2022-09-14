using Data.Context;
using DomainModels.Entities;

namespace DomainServices.Repositories
{
    public class CustomerRepositoryMySql
    {
        ///Unused because CustomerContext is out of patterns supported at design time
        private readonly CustomerContext _context;
        public CustomerRepositoryMySql(CustomerContext context)
        {
            _context = context;
        }
        virtual public int Create(Customer model)
        {

            _context.Set<Customer>().Add(model);
            _context.SaveChanges();

            return 201;
        }

        virtual public int Delete(int id)
        {
            _context.Set<Customer>().Remove(_context.Set<Customer>().Find(id));
            _context.SaveChanges();
            return 200;
        }

        virtual public List<Customer> GetAll()
        {
            return _context.Set<Customer>().ToList();
        }

        virtual public Customer GetSpecific(int id)
        {
            return _context.Set<Customer>().Find(id);
        }

        virtual public int Update(Customer model)
        {
            _context.Entry<Customer>(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return 200;
        }
        virtual public int Modify(Customer model)
        {
            _context.Entry<Customer>(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return 200;
        }
    }
}
