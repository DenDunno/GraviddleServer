using GraviddleServer.Code.Bot.StateMachineNM.StatesNM.StatisticsGeneration.Data;

namespace GraviddleServer.Code.Bot.StateMachineNM.StatesNM.StatisticsGeneration;

public interface IConfigProvider
{
    ChartConfig GetConfig();
}