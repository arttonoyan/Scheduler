using System;
using System.Threading;
using System.Threading.Tasks;

namespace Artnix.Scheduler.ConsoleTestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IJobService myJobService1 = JobManager.Scheduler()
                .ToRunOnceIn(3)
                .Seconds()
                .AtStartTime()
                .BuildJobService<MyJobGreen>();

            IJobService myJobService2 = JobManager.Scheduler()
                .ToRunOnceIn(1)
                .Seconds()
                .AtTheEndOfDay()
                .BuildJobService<MyJobRed>();

            IAsyncJobService asyncJobService = JobManager.Scheduler()
                .ToRunOnceIn(1)
                .Seconds()
                .AtStartTime()
                .BuildAsyncJobService<MyAsyncJob>();

            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            myJobService1.Start(token);
            myJobService2.Start(token);
            await asyncJobService.StartAsync(token);

            Thread.Sleep(15000);

            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();

            Console.ReadLine();
        }
    }

    public class MyAsyncJob : IAsyncJob
    {
        public Task ExecuteAsync()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(DateTime.Now.ToString("yyyy MM dd HH:mm:ss"));
            Console.ResetColor();
            return Task.CompletedTask;
        }
    }

    public class MyJobRed : IJob
    {
        public void Execute()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(DateTime.Now.ToString("yyyy MM dd HH:mm:ss"));
            Console.ResetColor();
        }
    }

    public class MyJobGreen : IJob
    {
        public void Execute()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(DateTime.Now.ToString("yyyy MM dd HH:mm:ss"));
            Console.ResetColor();
        }
    }
}
