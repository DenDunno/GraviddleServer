namespace GraviddleServer.Code.Bot.StateMachineNM.StatesNM.StatisticsGeneration;

public interface IPNGProvider
{
    Task<byte[]> Evaluate();
}