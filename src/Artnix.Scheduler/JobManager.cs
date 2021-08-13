using Artnix.Scheduler.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Artnix.Scheduler
{
    public static class JobManager
    {
        private readonly static HashSet<JobInfo> _jobs;

        static JobManager()
        {
            _jobs = new HashSet<JobInfo>();
        }

        public static IJobStartBuilder Scheduler()
            => new JobServiceBuilder();

        public static void AddJob<TJob>()
            where TJob : class, IJob
        {
            _jobs.Add(JobInfo.Sync<TJob>());
        }

        public static void AddAsyncJob<TJob>()
            where TJob : class, IAsyncJob
        {
            _jobs.Add(JobInfo.Async<TJob>());
        }

        public static HashSet<JobInfo> GetJobs() => _jobs;
        public static IEnumerable<Type> GetAsyncJobs() => _jobs.Filter(j => j.IsAsync);
        public static IEnumerable<Type> GetSyncJobs() => _jobs.Filter(j => !j.IsAsync);

        private static IEnumerable<Type> Filter(this IEnumerable<JobInfo> source, Func<JobInfo, bool> predicate)
            => source.Where(predicate).Select(j => j.JobType);
    }
}