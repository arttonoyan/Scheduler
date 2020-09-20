using Microsoft.Extensions.DependencyInjection;
using System;

namespace Artnix.Scheduler.DependencyInjection.ConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddScheduler(schedule =>
            {
                schedule.CreateJobService<MyJobRed>(cfg => cfg.ToRunOnceIn(2).Seconds().AtStartTime());
                schedule.CreateJobService<MyJobGreen>(cfg => cfg.ToRunOnceIn(3).Seconds().AtStartTime());
            });

            var provider = services.BuildServiceProvider();
            var scheduler = provider.GetService<IScheduler>();
            scheduler.Start();

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
