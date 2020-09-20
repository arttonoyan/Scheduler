using System;
using System.Threading;

namespace Artnix.Scheduler
{
    public interface IJobService : IDisposable
    {
        void Stop(CancellationToken cancellationToken);
        void Start(CancellationToken cancellationToken);
        bool Change(int dueTime, int period);
    }
}