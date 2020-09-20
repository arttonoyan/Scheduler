using System.Threading;
using System.Threading.Tasks;

namespace Artnix.Scheduler
{
    public interface IAsyncJobService
    {
        Task StopAsync(CancellationToken cancellationToken);
        Task StartAsync(CancellationToken cancellationToken);
        Task<bool> ChangeAsync(int dueTime, int period);
    }
}
