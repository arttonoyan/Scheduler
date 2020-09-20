using Microsoft.Extensions.DependencyInjection;
using System;

namespace Artnix.Scheduler
{
    public class ScopedJobService<TJob> : JobService, IDisposable
        where TJob : IJob
    {
        public ScopedJobService(int dueTime, int period, IServiceScopeFactory serviceScopeFactory)
            : base(dueTime, period)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        private readonly IServiceScopeFactory serviceScopeFactory;

        protected override void Execute()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var job = scope.ServiceProvider.GetService<TJob>();
            job.Execute();
        }
    }
}
