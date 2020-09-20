using System;
using System.Threading;
using System.Threading.Tasks;

namespace Artnix.Scheduler
{
    public abstract class AsyncJobService : IAsyncJobService, IAsyncDisposable
    {
        private Timer _timer;
        private int _dueTime;
        private int _period;

        public AsyncJobService(int dueTime, int period)
        {
            _dueTime = dueTime;
            _period = period;
        }

        protected abstract Task ExecuteAsync();

        public Task StartAsync(CancellationToken cancellationToken)
        {
            System.Diagnostics.Debug.WriteLine($"Timer was started on {DateTime.Now.ToString("yyyy MM dd HH:mm:ss")}");

            _timer = new Timer(async state =>
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    await StopAsync(cancellationToken);
                }
                else
                {
                    await ExecuteAsync();
                }
            }, null, _dueTime, _period);

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            System.Diagnostics.Debug.WriteLine($"Timer was stoped on {DateTime.Now.ToString("yyyy MM dd HH:mm:ss")}");
            await DisposeAsync();
        }

        public Task<bool> ChangeAsync(int dueTime, int period) => Task.Factory.StartNew(() =>
        {
            _dueTime = dueTime;
            _period = period;

            if (_timer != null)
                return _timer.Change(dueTime, period);

            return false;
        });

        public ValueTask DisposeAsync() => _timer.DisposeAsync();
    }
}
