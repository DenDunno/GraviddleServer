namespace TelegramBotNM.Utils;

public interface IFactory<out T>
{
    T Create();
}