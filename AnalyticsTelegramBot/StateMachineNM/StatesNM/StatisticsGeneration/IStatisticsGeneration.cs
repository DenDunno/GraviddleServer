namespace AnalyticsTelegramBot.StateMachineNM.StatesNM.StatisticsGeneration;

public interface IStatisticsGeneration
{
    IPNGProvider TimePerLevel { get; }
    IPNGProvider DeathPerLevel { get; }
    IPNGProvider StarsPerLevel { get; }
}