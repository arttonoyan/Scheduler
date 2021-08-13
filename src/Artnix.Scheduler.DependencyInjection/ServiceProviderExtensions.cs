using Microsoft.Extensions.DependencyInjection;
using System;

namespace Artnix.Scheduler
{
    public static class ServiceProviderExtensions
    {
        public static IAsyncJobService GetAsyncJobService(this IServiceProvider serviceProvider, Type jobType)
        {
            var jobServiceType = typeof(ScopedAsyncJobService<>).MakeGenericType(jobType);
            return (IAsyncJobService)serviceProvider.GetRequiredService(jobServiceType);
        }

        public static IJobService GetJobService(this IServiceProvider serviceProvider, Type jobType)
        {
            var jobServiceType = typeof(ScopedJobService<>).MakeGenericType(jobType);
            return (IJobService)serviceProvider.GetRequiredService(jobServiceType);
        }
    }
}
