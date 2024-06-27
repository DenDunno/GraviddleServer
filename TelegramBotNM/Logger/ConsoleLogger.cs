namespace GraviddleServer.Code.Logger;

public class ConsoleLogger : ILogger
{
    public Task Log(string text)
    {
        Console.WriteLine(text);
        return Task.CompletedTask;
    }
}