using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ChallengeFirst
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CustomerContext>(options =>
            options.UseMySql(
                    ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")),
                    b => b.MigrationsAssembly(typeof(CustomerContext)
                            .Assembly.FullName)));
            services.AddAutoMapper(typeof(Startup));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
