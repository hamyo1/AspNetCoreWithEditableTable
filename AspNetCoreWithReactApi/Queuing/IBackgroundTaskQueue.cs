using RazorPageTableProssesor.Models;

namespace AspNetCoreWithReactApi.Queuing
{

    public interface IBackgroundTaskQueue
    {
        Task QueueBackgroundWorkItem(ActivityEventArgs workItem);

        Task<ActivityEventArgs> DequeueAsync(CancellationToken cancellationToken);
    }

}
