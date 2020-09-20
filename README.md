# Artnix.SchedulerFramework
[![GitHub](https://img.shields.io/github/license/arttonoyan/Scheduler.svg)](https://github.com/arttonoyan/Scheduler/blob/master/LICENSE)
[![Nuget](https://img.shields.io/nuget/v/Artnix.SchedulerFramework.svg)](https://www.nuget.org/packages/Artnix.SchedulerFramework/)
[![Nuget](https://img.shields.io/nuget/dt/Artnix.SchedulerFramework.svg)](https://www.nuget.org/packages/Artnix.SchedulerFramework/)

# Artnix.SchedulerFramework.DependencyInjection
[![GitHub](https://img.shields.io/github/license/arttonoyan/Scheduler.svg)](https://github.com/arttonoyan/Scheduler/blob/master/LICENSE)
[![Nuget](https://img.shields.io/nuget/v/Artnix.SchedulerFramework.DependencyInjection.svg)](https://www.nuget.org/packages/Artnix.SchedulerFramework.DependencyInjection/)
[![Nuget](https://img.shields.io/nuget/dt/Artnix.SchedulerFramework.DependencyInjection.svg)](https://www.nuget.org/packages/Artnix.SchedulerFramework.DependencyInjection/)

## At the first you must create your job and implement the "Execute" function.

```
public class MyJob : IJob
{
    public void Execute()
    {
        Console.WriteLine(DateTime.Now.ToString("yyyy MM dd HH:mm:ss"));
    }
}
```
## DependencyInjection
```
services.AddScheduler(cfg =>
{
    cfg.CreaJobService<MyJobRed>(b => b.ToRunOnceIn(2).Seconds().AtStartTime());
    cfg.CreaJobService<MyJobGreen>(b => b.ToRunOnceIn(3).Seconds().AtStartTime());
});
```
```
var provider = services.BuildServiceProvider();
var scheduler = provider.GetService<IScheduler>();
scheduler.Start();
```

## Job service configuration.

```
IJobService myJobService1 = JobManager.Scheduler()
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

```
myJobService.Start(token);
<!--- or --->
await myJobService.StartAsync(token);
```
