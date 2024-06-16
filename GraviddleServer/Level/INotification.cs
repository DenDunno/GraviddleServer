namespace GraviddleServer.Level;

public interface INotification
{
    Task Notify(string text);
}