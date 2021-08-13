using Microsoft.Extensions.DependencyInjection;
using System;

namespace Artnix.Scheduler.Builders
{
    public interface IScheduleBuilder
    {
        IScheduleBuilder CreateJobService<TJob>(Action<IJobStartBuilder> confifurator)
            where TJob : class, IJob;
        IScheduleBuilder CreateAsyncJobService<TJob>(Action<IJobStartBuilder> confifurator)
            where TJob : class, IAsyncJob;
    }

    public class ScheduleBuilder : IScheduleBuilder
    {
        private readonly IServiceCollection services;

        public ScheduleBuilder(IServiceCollection services)
        {
            this.services = services;
        }

        public IScheduleBuilder CreateJobService<TJob>(Action<IJobStartBuilder> confifurator)
            where TJob : class, IJob
        {
            JobManager.AddJob<TJob>();

            var builder = new JobServiceBuilder();
            confifurator.Invoke(builder);

            var options = builder.Build();
            services.AddSingleton(p => new ScopedJobService<TJob>(options.DueTime, options.Period, p.GetService<IServiceScopeFactory>()));
            services.AddTransient<TJob>();

            return this;
        }

        public IScheduleBuilder CreateAsyncJobService<TJob>(Action<IJobStartBuilder> confifurator)
            where TJob : class, IAsyncJob
        {
            JobManager.AddAsyncJob<TJob>();

            var builder = new JobServiceBuilder();
            confifurator.Invoke(builder);

            var options = builder.Build();
            services.AddSingleton(p => new ScopedAsyncJobService<TJob>(options.DueTime, options.Period, p.GetService<IServiceScopeFactory>()));
            services.AddTransient<TJob>();

            return this;
        }
    }
}
