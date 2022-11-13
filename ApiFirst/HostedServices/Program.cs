using AppServices.Interfaces;
using AppServices.Services;
using DomainServices.Interfaces;
using DomainServices.Services;
using EntityFrameworkCore.UnitOfWork.Extensions;
using HostedServices;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddCronJob<OrdersCronJob>(config =>
        {
            config.CronExpression = "0 10 * * *";
            config.TimeZoneInfo = TimeZoneInfo.Local;
        });
        var connectionString = hostContext.Configuration.GetConnectionString("DefaultConnection");

        services.AddTransient<ICustomerAppService, CustomerAppService>();
        services.AddTransient<IProductAppService, ProductAppService>();
        services.AddTransient<ICustomerBankInfoAppService, CustomerBankInfoAppService>();
        services.AddTransient<IOrderAppService, OrderAppService>();
        services.AddTransient<IPortfolioProductAppService, PortfolioProductAppService>();
        services.AddTransient<IPortfolioAppService, PortfolioAppService>();
        services.AddTransient<ICustomerService, CustomerService>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<ICustomerBankInfoService, CustomerBankInfoService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IPortfolioProductService, PortfolioProductService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IPortfolioService, PortfolioService>();
        services.AddDbContext<WarrenContext>(
            options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)), contextLifetime: ServiceLifetime.Transient);
        services.AddUnitOfWork<WarrenContext>(ServiceLifetime.Transient);
    })
    .Build();

await host.RunAsync();
