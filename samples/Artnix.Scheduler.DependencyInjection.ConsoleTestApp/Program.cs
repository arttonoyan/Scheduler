using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Artnix.Scheduler.DependencyInjection.ConsoleTestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddScheduler(schedule =>
            {
                schedule.CreateAsyncJobService<MyJob>(cfg => cfg.ToRunOnceIn(2).Seconds().AtStartTime());
                schedule.CreateJobService<MyJobRed>(cfg => cfg.ToRunOnceIn(2).Seconds().AtStartTime());
                schedule.CreateJobService<MyJobGreen>(cfg => cfg.ToRunOnceIn(3).Seconds().AtStartTime());
            });

            var provider = services.BuildServiceProvider();
            var scheduler = provider.GetService<IScheduler>();
            await scheduler.StartAsync();

            Console.ReadLine();
        }
    }

    public class MyJob : IAsyncJob
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
