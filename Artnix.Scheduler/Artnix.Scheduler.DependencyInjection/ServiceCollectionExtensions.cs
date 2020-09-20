using Artnix.Scheduler.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Artnix.Scheduler
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddScheduler(this IServiceCollection services, Action<IJobMapperBuilder> confifurator)
        {
            services.AddScoped<IScheduler, Scheduler>();

            var builder = new JobMapperBuilder(services);
            confifurator.Invoke(builder);
            return services;
        }
    }
}
