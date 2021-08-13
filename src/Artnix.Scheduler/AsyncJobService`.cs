using System;
using System.Threading.Tasks;

namespace Artnix.Scheduler
{
    public class AsyncJobService<TJob> : AsyncJobService
        where TJob : IAsyncJob
    {
        private readonly IAsyncJob _job;

        public AsyncJobService(int dueTime, int period, Func<TJob> createJob) : base(dueTime, period)
        {
            _job = createJob();
        }

        protected override Task ExecuteAsync() => _job.ExecuteAsync();
    }
}
