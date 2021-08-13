using Artnix.Scheduler.Builders;
using System;

namespace Artnix.Scheduler
{
    public static class IJobServiceBuilderExtensions
    {
        public static IJobService BuildJobService<TJob>(this IJobServiceBuilder builder)
            where TJob : IJob, new()
            => builder.BuildJobService(() => new TJob());

        public static IJobService BuildJobService<TJob>(this IJobServiceBuilder builder, Func<TJob> createJob)
            where TJob : IJob
        {
            var options = builder.Build();
            return new JobService<TJob>(options.DueTime, options.Period, createJob);
        }

        public static IAsyncJobService BuildAsyncJobService<TJob>(this IJobServiceBuilder builder)
            where TJob : IAsyncJob, new()
            => builder.BuildAsyncJobService(() => new TJob());

        public static IAsyncJobService BuildAsyncJobService<TJob>(this IJobServiceBuilder builder, Func<TJob> createJob)
            where TJob : IAsyncJob
        {
            var options = builder.Build();
            return new AsyncJobService<TJob>(options.DueTime, options.Period, createJob);
        }
    }
}
