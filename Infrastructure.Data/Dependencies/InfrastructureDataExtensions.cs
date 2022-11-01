using EntityFrameworkCore.UnitOfWork.Extensions;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.Dependencies
{
    public static class InfrastructureDataExtensions
    {
        public static IServiceCollection AddInfrastructureDataDependencies(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");

            services.AddDbContext<WarrenContext>(
                options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)), contextLifetime: ServiceLifetime.Transient);
            services.AddUnitOfWork<WarrenContext>(ServiceLifetime.Transient);

            return services;
        }
    }
}
