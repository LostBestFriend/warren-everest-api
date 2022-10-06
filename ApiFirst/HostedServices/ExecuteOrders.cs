using AppServices.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AAaa
{
    public class ExecuteOrders : CronService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ExecuteOrders> _logger;
        public ExecuteOrders(IScheduleConfig<ExecuteOrders> config,
                             IServiceScopeFactory scopeFactory,
                             ILogger<ExecuteOrders> logger)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
            _logger = logger;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public override void DoWork(CancellationToken cancellationToken)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IPortfolioAppService>();
                repository.ExecuteTodaysOrdersAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro na execução das ordens do dia");
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
