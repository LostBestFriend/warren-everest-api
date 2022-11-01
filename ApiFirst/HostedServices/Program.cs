using AppServices.Dependencies;
using DomainServices.Dependencies;
using HostedServices;
using Infrastructure.Data.Dependencies;
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
        AppServicesExtensions.AddAppServicesDependencies(services);
        DomainServicesExtensions.AddDomainServiceDependencies(services);
        InfrastructureDataExtensions.AddInfrastructureDataDependencies(services, hostContext.Configuration);
    })
    .Build();

await host.RunAsync();
