namespace GraviddleServer.Code.Level;

public interface INotification
{
    Task Notify(string text);
}