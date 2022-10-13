using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data.Context
{
    public class WarrenContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.Load("Infrastructure.Data"));
        }

        public WarrenContext(DbContextOptions<WarrenContext> options) : base(options)
        { }
    }
}
