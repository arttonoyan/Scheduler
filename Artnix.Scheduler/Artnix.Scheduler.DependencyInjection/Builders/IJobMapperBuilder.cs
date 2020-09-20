using Microsoft.Extensions.DependencyInjection;
using System;

namespace Artnix.Scheduler.Builders
{
    public interface IJobMapperBuilder
    {
        IJobMapperBuilder CreateJobService<TJob>(Action<IJobStartBuilder> confifurator)
            where TJob : class, IJob;
    }

    public class JobMapperBuilder : IJobMapperBuilder
    {
        private readonly IServiceCollection services;

        public JobMapperBuilder(IServiceCollection services)
        {
            this.services = services;
        }

        public IJobMapperBuilder CreateJobService<TJob>(Action<IJobStartBuilder> confifurator)
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
    }
}
