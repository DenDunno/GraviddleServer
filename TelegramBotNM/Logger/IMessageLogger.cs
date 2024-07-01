namespace TelegramBotNM.Logger;

public interface IMessageLogger
{
    Task Log(string text);
}