# Artnix.SchedulerFramework
[![GitHub](https://img.shields.io/github/license/arttonoyan/Scheduler.svg)](https://github.com/arttonoyan/Scheduler/blob/master/LICENSE)
[![Nuget](https://img.shields.io/nuget/v/Artnix.SchedulerFramework.svg)](https://www.nuget.org/packages/Artnix.SchedulerFramework/)
[![Nuget](https://img.shields.io/nuget/dt/Artnix.SchedulerFramework.svg)](https://www.nuget.org/packages/Artnix.SchedulerFramework/)

# Artnix.SchedulerFramework.DependencyInjection
[![GitHub](https://img.shields.io/github/license/arttonoyan/Scheduler.svg)](https://github.com/arttonoyan/Scheduler/blob/master/LICENSE)
[![Nuget](https://img.shields.io/nuget/v/Artnix.SchedulerFramework.DependencyInjection.svg)](https://www.nuget.org/packages/Artnix.SchedulerFramework.DependencyInjection/)
[![Nuget](https://img.shields.io/nuget/dt/Artnix.SchedulerFramework.DependencyInjection.svg)](https://www.nuget.org/packages/Artnix.SchedulerFramework.DependencyInjection/)

## At the first you must create your Job and implement the `Execute` function.

```
public class MyJobRed : IJob
{
    public void Execute()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(DateTime.Now.ToString("yyyy MM dd HH:mm:ss"));
        Console.ResetColor();
    }
}
```
## Or AsyncJob and implement the `ExecuteAsync` function.
```
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

```
## DependencyInjection
```
services.AddScheduler(schedule =>
{
    schedule.CreateAsyncJobService<MyAsyncJob>(cfg => cfg.ToRunOnceIn(2).Seconds().AtStartTime());
    schedule.CreaJobService<MyJobRed>(cfg => cfg.ToRunOnceIn(2).Seconds().AtStartTime());
    schedule.CreaJobService<MyJobGreen>(cfg => cfg.ToRunOnceIn(3).Seconds().AtStartTime());
});
```
```
var provider = services.BuildServiceProvider();
var scheduler = provider.GetService<IScheduler>();
scheduler.StartAsync();
```
## Async Job service configuration.

```
IAsyncJobService asyncJobService = JobManager.Scheduler()
    .ToRunOnceIn(1)
    .Seconds()
    .AtStartTime()
    .BuildAsyncJobService<MyAsyncJob>();
```
## Use AsyncJobService
```
var cancellationTokenSource = new CancellationTokenSource();
var token = cancellationTokenSource.Token;
```

`await asyncJobService.StartAsync(token);`

## Job service configuration.

```
IJobService myJobService = JobManager.Scheduler()
    .ToRunOnceIn(5)
    .Seconds()
    .AtStartTime()
    .BuildJobService<MyJob>();
```

## Use JobService

```
var cancellationTokenSource = new CancellationTokenSource();
var token = cancellationTokenSource.Token;
```

`myJobService.Start(token);`
