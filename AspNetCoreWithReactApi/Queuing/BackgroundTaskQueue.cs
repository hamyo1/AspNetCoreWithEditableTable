using RazorPageTableProssesor.Models;
using System.Collections.Concurrent;

namespace AspNetCoreWithReactApi.Queuing
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        readonly ConcurrentQueue<ActivityEventArgs> _workItems = new ConcurrentQueue<ActivityEventArgs>();
        readonly SemaphoreSlim _signal = new SemaphoreSlim(0);
        readonly ILogger<BackgroundTaskQueue> _logger;

        public BackgroundTaskQueue(ILogger<BackgroundTaskQueue> logger)
        {
            _logger = logger;
        }
        public async Task QueueBackgroundWorkItem(ActivityEventArgs workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }
            _logger.LogInformation("Enqueue the ActivityEventArgs");
            _workItems.Enqueue(workItem);
            _signal.Release();
            await Task.CompletedTask;
        }

        public async Task<ActivityEventArgs> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _workItems.TryDequeue(out var workItem);

            return workItem;
        }
    }
}
