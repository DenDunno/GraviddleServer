namespace GraviddleServer.Code.API;

public interface INotification
{
    Task Notify(string text);
}