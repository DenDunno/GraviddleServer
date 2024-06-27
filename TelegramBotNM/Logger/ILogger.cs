namespace GraviddleServer.Code.Logger;

public interface ILogger
{
    Task Log(string text);
}