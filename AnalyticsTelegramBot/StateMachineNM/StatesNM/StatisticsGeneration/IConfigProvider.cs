using AnalyticsTelegramBot.StateMachineNM.StatesNM.StatisticsGeneration.Data;

namespace AnalyticsTelegramBot.StateMachineNM.StatesNM.StatisticsGeneration;

public interface IConfigProvider
{
    Task<ChartConfig> GetConfig();
}