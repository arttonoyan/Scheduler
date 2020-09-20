using Artnix.Scheduler.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Artnix.Scheduler
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddScheduler(this IServiceCollection services, Action<IScheduleBuilder> confifurator)
        {
            services.AddScoped<IScheduler, Scheduler>();

            var builder = new ScheduleBuilder(services);
            confifurator.Invoke(builder);
            return services;
        }
    }
}
