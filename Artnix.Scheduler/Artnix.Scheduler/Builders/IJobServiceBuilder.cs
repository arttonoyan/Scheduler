using System;

namespace Artnix.Scheduler.Builders
{
    public interface IJobServiceBuilder
    {
        IJobService BuildJobService<TJob>() where TJob : IJob, new();
        IJobService BuildJobService<TJob>(Func<TJob> createJob) where TJob : IJob;
    }
}