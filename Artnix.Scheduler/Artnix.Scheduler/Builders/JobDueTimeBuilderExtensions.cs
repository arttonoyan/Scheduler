using Artnix.Scheduler.Builders;
using System;

namespace Artnix.Scheduler
{
    public static class JobDueTimeBuilderExtensions
    {
        public static IJobServiceBuilder AtTheEndOfDay(this IJobDueTimeBuilder builder)
            => builder.At(DateTime.Today.AddDays(1).AddMilliseconds(-1));

        public static IJobServiceBuilder AtTomorrowStartOfDay(this IJobDueTimeBuilder builder)
            => builder.At(DateTime.Today.AddDays(1));
    }
}
