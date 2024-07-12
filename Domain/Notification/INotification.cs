namespace Domain.Notification;

public interface INotification<in T>
{
    Task Notify(T record);
}