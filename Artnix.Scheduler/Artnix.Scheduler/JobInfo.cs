using System;

namespace Artnix.Scheduler
{
    public class JobInfo
    {
        public Type JobType { get; set; }
        public bool IsAsync { get; set; }

        private JobInfo(bool isAsync, Type jobType)
        {
            IsAsync = isAsync;
            JobType = jobType;
        }

        public static JobInfo Async<TJob>() where TJob : IAsyncJob
            => Async(typeof(TJob));

        public static JobInfo Sync<TJob>() where TJob : IJob
            => Sync(typeof(TJob));

        public static JobInfo Async(Type handlerType)
        {
            return new JobInfo(true, handlerType);
        }
        public static JobInfo Sync(Type handlerType)
        {
            return new JobInfo(false, handlerType);
        }
    }
}
