using DomainModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class CustomerContext : DbContext
    {

        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customer { get; set; }
    }
}
