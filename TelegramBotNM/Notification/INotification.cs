namespace TelegramBotNM.Notification;

public interface INotification
{
    Task Notify(string text);
}