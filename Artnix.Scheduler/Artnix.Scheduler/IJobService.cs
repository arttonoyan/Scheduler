using System;
using System.Threading;
using System.Threading.Tasks;

namespace Artnix.Scheduler
{
    public interface IJobService : IDisposable
    {
        void Stop(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
        void Start(CancellationToken cancellationToken);
        Task StartAsync(CancellationToken cancellationToken);
        Task<bool> ChangeAsync(int dueTime, int period);
    }
}