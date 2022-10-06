using AAaa;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddCronJob<ExecuteOrders>(config =>
        {
            config.CronExpression = "0 10 * * *";
            config.TimeZoneInfo = TimeZoneInfo.Local;
        });
    })
    .Build();

await host.RunAsync();
