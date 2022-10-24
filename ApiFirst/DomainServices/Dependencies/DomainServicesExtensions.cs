using DomainServices.Interfaces;
using DomainServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DomainServices.Dependencies
{
    public class DomainServicesExtensions
    {
        public static IServiceCollection AddDomainServiceDependencies(IServiceCollection services)
        {
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICustomerBankInfoService, CustomerBankInfoService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPortfolioProductService, PortfolioProductService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPortfolioService, PortfolioService>();

            return services;
        }
    }
}
