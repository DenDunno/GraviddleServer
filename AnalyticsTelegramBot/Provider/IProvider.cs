namespace AnalyticsTelegramBot.Provider;

public interface IProvider<out T>
{
    T Provide();
}