namespace TelegramBotNM.Notification;

public interface INotification<in T>
{
    Task Notify(T record);
}