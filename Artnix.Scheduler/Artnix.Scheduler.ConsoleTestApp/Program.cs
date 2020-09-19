using System;
using System.Threading;

namespace Artnix.Scheduler.ConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
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

            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            myJobService1.Start(token);
            myJobService2.Start(token);

            Thread.Sleep(15000);

            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();

            Console.ReadLine();
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
