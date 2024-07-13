namespace AnalyticsTelegramBot.Provider;

public interface IProvider<T>
{
    Task<T> Provide();
}