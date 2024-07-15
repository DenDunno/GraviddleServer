using Coravel.Scheduling.Schedule.Interfaces;

namespace GraviddleServer.Code.Refresh;

public interface ISchedulerBuilder
{
    void Build(IScheduler scheduler);
}