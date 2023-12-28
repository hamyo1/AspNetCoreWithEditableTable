using Microsoft.Extensions.Configuration;

namespace AspNetCoreWithReactApi.BackgroundTasks
{
    public class SomeScheduler : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<SomeScheduler> _logger;
        private Timer? _timer = null;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public SomeScheduler(ILogger<SomeScheduler> logger, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");
            string doNotCallMeCheckSchedulerHourToStartRun = _configuration["SchedulerHourToStartRun"];//"15:19" (string)
            double doNotCallMeCheckSchedulerTimeSpanInHours = double.Parse(_configuration["SchedulerTimeSpanInHours"]);//24 (int)

            DateTime dateTime = DateTime.Now;
            string startTime = dateTime.ToString("HH:mm");

            TimeSpan duration = DateTime.Parse(doNotCallMeCheckSchedulerHourToStartRun).Subtract(DateTime.Parse(startTime));

            if(duration<TimeSpan.Zero)
            {
                duration = duration.Add(new TimeSpan(0,24,0,0));
            }
            _logger.LogInformation($"next schedule runing duration: {duration.ToString(@"hh\:mm\:ss")}");
            //_timer = new Timer(DoWork, cancellationToken, TimeSpan.Zero,
            //    TimeSpan.FromHours(doNotCallMeCheckSchedulerTimeSpanInHours));
            _timer = new Timer(DoWork, cancellationToken, TimeSpan.FromSeconds(duration.TotalSeconds),
                TimeSpan.FromHours(doNotCallMeCheckSchedulerTimeSpanInHours));

            await Task.CompletedTask;
        }

        async void DoWork(object? state)
        {
            var count = Interlocked.Increment(ref executionCount);

            if(state != null)
            {
                CancellationToken cancellationToken = (CancellationToken)state;
                if (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        _logger.LogInformation($"start schedule task MainProcess in {DateTime.Now.ToString("yyy-MM-dd HH:mm")}");
                        //await _someService.DoNotCallMeMainProcess();
                        ////using (var scope = _serviceProvider.CreateScope())
                        ////{
                        ////    var doNotCallMeService = scope.ServiceProvider.GetRequiredService<IDoNotCallMeService>();

                        ////    await doNotCallMeService.DoNotCallMeMainProcess();
                        ////}
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error occurred while executing MainProcess");
                    }
                }
            }
        }

        public async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Scheduler Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
