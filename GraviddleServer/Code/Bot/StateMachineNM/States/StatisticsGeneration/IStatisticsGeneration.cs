namespace GraviddleServer.Code.Bot.StateMachineNM.States;

public interface IStatisticsGeneration
{
    IPNGProvider TimePerLevel { get; }
    IPNGProvider DeathPerLevel { get; }
    IPNGProvider StarsPerLevel { get; }
}