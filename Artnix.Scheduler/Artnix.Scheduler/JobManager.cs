using Artnix.Scheduler.Builders;
using System;
using System.Collections.Generic;

namespace Artnix.Scheduler
{
    public static class JobManager
    {
        private readonly static HashSet<Type> _jobs;

        static JobManager()
        {
            _jobs = new HashSet<Type>();
        }

        public static IJobStartBuilder Scheduler()
            => new JobServiceBuilder();

        public static void AddJob<TJob>()
            where TJob : class, IJob
        {
            _jobs.Add(typeof(TJob));
        }

        public static HashSet<Type> GetJobs() => _jobs;
    }
}