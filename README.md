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
