
using AspNetCoreWithReactApi.Queuing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RazorPageTableProssesor.Interfaces;
using RazorPageTableProssesor.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lubinski.Commbox.API.BackgroundTasks
{

    public class QueuedHostedService : BackgroundService
    {
        readonly ILogger<QueuedHostedService> _logger;
        private IServiceProvider Services { get; }
        private IBackgroundTaskQueue TaskQueue { get; }

        public QueuedHostedService(IBackgroundTaskQueue taskQueue, IServiceProvider services, ILogger<QueuedHostedService> logger)
        {
            TaskQueue = taskQueue;
            Services = services;
            _logger = logger;
        }


        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Queued Hosted Service is starting.");

            while (!cancellationToken.IsCancellationRequested)
            {
                ActivityEventArgs workItem;
                long commboxObjId = -1;
                try
                {
                    workItem = await TaskQueue.DequeueAsync(cancellationToken);
                    _logger.LogInformation($"Dequeue workItem for  object id:");
                    using (var scope = Services.CreateScope())
                    {
                        var customerConversation = scope.ServiceProvider.GetRequiredService<ICustomerConversation>();
                        bool success = await customerConversation.OnNewActivity(workItem);
                        if (success)
                        {
                            _logger.LogInformation($"event handling for  object idended successfully");
                        }
                        else
                        {
                            _logger.LogError($"event handling for  object id: Failed");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error occurred executing when try Dequeue workItem :{ex.Message}, {ex.StackTrace}");
                }
            }

            _logger.LogDebug("Queued Hosted Service is stopping.");
        }
    }
}
