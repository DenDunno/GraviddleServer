namespace AnalyticsTelegramBot.StateMachineNM.StatesNM.StatisticsGeneration;

public interface IPNGProvider
{
    Task<byte[]> Evaluate();
}