namespace TelegramBotNM.Notification;

public interface IFormatter<in TInput>
{
    string Execute(TInput record);
}