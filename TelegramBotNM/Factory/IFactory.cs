namespace TelegramBotNM.Factory;

public interface IFactory<out T>
{
    T Create();
}