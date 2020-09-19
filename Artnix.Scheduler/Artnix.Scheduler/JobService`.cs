using System;
using System.Threading;
using System.Threading.Tasks;

namespace Artnix.Scheduler
{
    public class JobService<TJob> : IJobService, IDisposable
        where TJob : IJob
    {
        public JobService(int dueTime, int period, Func<TJob> createJob)
        {
            _job = createJob();
            _dueTime = dueTime;
            _period = period;
        }

        private Timer _timer;
        private TJob _job;
        private int _dueTime;
        private int _period;

        public bool Change(int dueTime, int period)
        {
            _dueTime = dueTime;
            _period = period;

            if (_timer != null)
                return _timer.Change(dueTime, period);

            return false;
        }

        public void Start(CancellationToken cancellationToken)
        {
            System.Diagnostics.Debug.WriteLine($"Timer was started on {DateTime.Now.ToString("yyyy MM dd HH:mm:ss")}");

            _timer = new Timer((state) =>
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Stop(cancellationToken);
                }
                else
                {
                    _job.Execute();
                }
            }, null, _dueTime, _period);
        }

        public void Stop(CancellationToken cancellationToken)
        {
            System.Diagnostics.Debug.WriteLine($"Timer was stoped on {DateTime.Now.ToString("yyyy MM dd HH:mm:ss")}");
            Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => Start(cancellationToken));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => Stop(cancellationToken));
        }

        public Task<bool> ChangeAsync(int dueTime, int period)
        {
            return Task.Factory.StartNew(() => Change(dueTime, period));
        }

        #region IDisposable
        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_timer == null)
                return;

            if (disposing)
                _timer.Dispose();
            _timer = null;
        }
        #endregion
    }
}