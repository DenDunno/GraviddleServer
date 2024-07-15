using Coravel.Invocable;
using Coravel.Scheduling.Schedule.Interfaces;
namespace GraviddleServer.Code.Refresh;

public class FifteenMinutesSchedule : ISchedulerBuilder
{
    private readonly IInvocable _invocable;

    public FifteenMinutesSchedule(IInvocable invocable)
    {
        _invocable = invocable;
    }

    public void Build(IScheduler scheduler)
    {
        scheduler.ScheduleAsync(_invocable.Invoke).EveryFifteenMinutes();
    }
}