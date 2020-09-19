using Artnix.Scheduler.Builders;

namespace Artnix.Scheduler
{
    public static class JobManager
    {
        public static IJobStartBuilder Scheduler()
            => new JobServiceBuilder();
    }
}