namespace Artnix.Scheduler.Builders
{
    public interface IJobStartBuilder
    {
        IJobIntervalDateBuilder ToRunOnceIn(float interval);
    }
}