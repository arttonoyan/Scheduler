using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Artnix.Scheduler
{
    public interface IScheduler
    {
        Task StopAsync(CancellationToken cancellationToken = default);
        Task StartAsync(CancellationToken cancellationToken = default);
    }

    public class Scheduler : IScheduler
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Scheduler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var provider = scope.ServiceProvider;

            StartOrStop(provider, jobService => jobService.Start(cancellationToken));
            return StartOrStopAsync(provider, jobService => jobService.StartAsync(cancellationToken));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var provider = scope.ServiceProvider;

            StartOrStop(provider, jobService => jobService.Stop(cancellationToken));
            return StartOrStopAsync(provider, jobService => jobService.StopAsync(cancellationToken));
        }

        private void StartOrStop(IServiceProvider provider, Action<IJobService> action)
        {
            foreach (var job in JobManager.GetSyncJobs())
            {
                var jobServiceType = typeof(ScopedJobService<>).MakeGenericType(job);
                var jobService = (IJobService)provider.GetRequiredService(jobServiceType);
                action.Invoke(jobService);
            }
        }

        private Task StartOrStopAsync(IServiceProvider provider, Func<IAsyncJobService, Task> func)
        {
            var tasks = new List<Task>();
            foreach (var job in JobManager.GetAsyncJobs())
            {
                var jobServiceType = typeof(ScopedAsyncJobService<>).MakeGenericType(job);
                var jobService = (IAsyncJobService)provider.GetRequiredService(jobServiceType);
                var task = func.Invoke(jobService);
                tasks.Add(task);
            }

            return Task.FromResult(tasks);
        }
    }
}
