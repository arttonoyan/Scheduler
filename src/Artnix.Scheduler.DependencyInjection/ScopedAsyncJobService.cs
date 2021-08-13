using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Artnix.Scheduler
{
    public class ScopedAsyncJobService<TJob> : AsyncJobService
        where TJob : IAsyncJob
    {
        public ScopedAsyncJobService(int dueTime, int period, IServiceScopeFactory serviceScopeFactory)
            : base(dueTime, period)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        private readonly IServiceScopeFactory serviceScopeFactory;

        protected override Task ExecuteAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var job = scope.ServiceProvider.GetService<TJob>();
            return job.ExecuteAsync();
        }
    }
}
