using System;

namespace Artnix.Scheduler.Builders
{
    class JobServiceBuilder : IJobServiceBuilder, IJobIntervalDateBuilder, IJobDueTimeBuilder, IJobStartBuilder
    {
        private float _interval;
        private TimeUnit _timeUnit;
        private DateTime _atDateTime;
        private bool _atStartTime;

        public IJobService BuildJobService<TJob>()
            where TJob : IJob, new()
        {
            return BuildJobService(() => new TJob());
        }

        public IJobService BuildJobService<TJob>(Func<TJob> createJob)
            where TJob : IJob
        {
            int dueTime = CreateDueTime();
            int period = CreatePeriodTime();
            return new JobService<TJob>(dueTime, period, createJob);
        }

        private int CreatePeriodTime()
        {
            return (int)((int)_timeUnit * _interval);
        }

        private int CreateDueTime()
        {
            if (_atStartTime)
                return 0;

            TimeSpan time = _atDateTime - DateTime.Now;
            return (int)time.TotalMilliseconds;
        }
        
        public IJobIntervalDateBuilder ToRunOnceIn(float interval)
        {
            _interval = interval;
            return this;
        }

        public IJobDueTimeBuilder Seconds()
        {
            _timeUnit = TimeUnit.Second;
            return this;
        }

        public IJobDueTimeBuilder Minutes()
        {
            _timeUnit = TimeUnit.Minute;
            return this;
        }

        public IJobDueTimeBuilder Hours()
        {
            _timeUnit = TimeUnit.Hour;
            return this;
        }

        public IJobDueTimeBuilder Days()
        {
            _timeUnit = TimeUnit.Day;
            return this;
        }

        public IJobServiceBuilder At(DateTime dateTime)
        {
            _atDateTime = dateTime;
            return this;
        }

        public IJobServiceBuilder AtStartTime()
        {
            _atStartTime = true;
            return this;
        }

        public IJobServiceBuilder AtTheEndOfDay()
            => At(DateTime.Today.AddDays(1).AddMilliseconds(-1));

        public IJobServiceBuilder AtTomorrowStartOfDay()
            => At(DateTime.Today.AddDays(1));
    }
}
