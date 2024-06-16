using GraviddleServer.Level;
namespace GraviddleServer.TelegramBot;

public class TelegramBotNotification : INotification
{
    private readonly TelegramBotBridge _bridge;

    public TelegramBotNotification(TelegramBotBridge bridge)
    {
        _bridge = bridge;
    }

    public async Task Notify(string text)
    {
        await _bridge.SendToAll(text);
    }
}