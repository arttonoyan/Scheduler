using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Artnix.Scheduler
{
    public interface IScheduler
    {
        void Stop(CancellationToken cancellationToken = default);
        void Start(CancellationToken cancellationToken = default);

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

        public void Start(CancellationToken cancellationToken)
            => StartOrStop(jobService => jobService.Start(cancellationToken));

        public void Stop(CancellationToken cancellationToken)
            => StartOrStop(jobService => jobService.Stop(cancellationToken));

        private void StartOrStop(Action<IJobService> action)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var provider = scope.ServiceProvider;
            foreach (var job in JobManager.GetJobs())
            {
                var jobServiceType = typeof(ScopedJobService<>).MakeGenericType(job);
                var jobService = (IJobService)provider.GetRequiredService(jobServiceType);
                action.Invoke(jobService);
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
            => StartOrStopAsync(jobService => jobService.StartAsync(cancellationToken));

        public Task StopAsync(CancellationToken cancellationToken)
            => StartOrStopAsync(jobService => jobService.StopAsync(cancellationToken));

        private Task StartOrStopAsync(Func<IJobService, Task> func)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var provider = scope.ServiceProvider;

            var jobs = JobManager.GetJobs();
            var tasks = new List<Task>(jobs.Count);
            foreach (var job in jobs)
            {
                var jobServiceType = typeof(ScopedJobService<>).MakeGenericType(job);
                var jobService = (IJobService)provider.GetRequiredService(jobServiceType);
                var task = func.Invoke(jobService);
                tasks.Add(task);
            }

            return Task.FromResult(tasks);
        }
    }
}
