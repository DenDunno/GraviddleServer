namespace AnalyticsTelegramBot.StateMachineNM.StatesNM.StatisticsGeneration;

public interface IAlbumGeneration
{
    Task<byte[][]> Generate();
}