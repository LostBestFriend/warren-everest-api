using AppServices.Interfaces;
using AppServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AppServices.Dependencies
{
    public static class AppServicesExtensions
    {
        public static IServiceCollection AddAppServicesDependencies(IServiceCollection services)
        {
            services.AddTransient<ICustomerAppService, CustomerAppService>();
            services.AddTransient<IProductAppService, ProductAppService>();
            services.AddTransient<ICustomerBankInfoAppService, CustomerBankInfoAppService>();
            services.AddTransient<IOrderAppService, OrderAppService>();
            services.AddTransient<IPortfolioProductAppService, PortfolioProductAppService>();
            services.AddTransient<IPortfolioAppService, PortfolioAppService>();

            return services;
        }
    }
}
