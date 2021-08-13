namespace Artnix.Scheduler.Builders
{
    public interface IJobIntervalDateBuilder
    {
        IJobDueTimeBuilder Seconds();
        IJobDueTimeBuilder Minutes();
        IJobDueTimeBuilder Hours();
        IJobDueTimeBuilder Days();
    }
}