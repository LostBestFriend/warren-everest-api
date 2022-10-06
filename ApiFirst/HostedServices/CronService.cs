using Cronos;
using Microsoft.Extensions.Hosting;

namespace AAaa
{
    public class CronService : IHostedService, IDisposable
    {
        private System.Timers.Timer? _timer;
        private readonly CronExpression _expression;
        private readonly TimeZoneInfo _timeZoneInfo;

        public CronService(string cronExpression, TimeZoneInfo timeZoneInfo)
        {
            _expression = CronExpression.Parse(cronExpression);
            _timeZoneInfo = timeZoneInfo;
        }

        public virtual async Task StartAsync(CancellationToken stoppingToken)
        {
            await ScheduleJob(stoppingToken);
        }

        protected virtual async Task ScheduleJob(CancellationToken stoppingToken)
        {
            var next = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
            if (next.HasValue)
            {
                var delay = next.Value - DateTimeOffset.Now;
                if (delay.TotalMilliseconds <= 0)
                {
                    await ScheduleJob(stoppingToken);
                }
                _timer = new System.Timers.Timer(delay.TotalMilliseconds);
                _timer.Elapsed += async (sender, args) =>
                {
                    _timer.Dispose();
                    _timer = null;

                    if (!stoppingToken.IsCancellationRequested)
                    {
                        DoWork(stoppingToken);
                    }

                    if (!stoppingToken.IsCancellationRequested)
                    {
                        await ScheduleJob(stoppingToken);
                    }
                };
                _timer.Start();
            }
        }

        public virtual async void DoWork(CancellationToken stoppingToken)
        {
            await Task.Delay(5000, stoppingToken);
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Stop();
            await Task.CompletedTask;
        }

        public virtual void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
