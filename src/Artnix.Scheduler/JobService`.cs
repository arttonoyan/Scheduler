using System;

namespace Artnix.Scheduler
{
    public class JobService<TJob> : JobService, IDisposable
        where TJob : IJob
    {
        private readonly IJob _job;
        public JobService(int dueTime, int period, Func<TJob> createJob) : base(dueTime, period)
        {
            _job = createJob();
        }

        protected override void Execute() => _job.Execute();
    }
}