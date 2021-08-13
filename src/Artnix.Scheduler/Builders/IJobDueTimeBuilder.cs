using System;

namespace Artnix.Scheduler.Builders
{
    public interface IJobDueTimeBuilder
    {
        IJobServiceBuilder At(DateTime dateTime);
        IJobServiceBuilder AtStartTime();
    }
}