using HostedServices;
using Microsoft.Extensions.Hosting;
using System;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddCronJob<OrdersCronJob>(config =>
        {
            config.CronExpression = "0 10 * * *";
            config.TimeZoneInfo = TimeZoneInfo.Local;
        });
    })
    .Build();

await host.RunAsync();
