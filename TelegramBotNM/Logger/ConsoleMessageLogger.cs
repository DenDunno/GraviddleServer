namespace TelegramBotNM.Logger;

public class ConsoleMessageLogger : IMessageLogger
{
    public Task Log(string text)
    {
        Console.WriteLine(text);
        return Task.CompletedTask;
    }
}