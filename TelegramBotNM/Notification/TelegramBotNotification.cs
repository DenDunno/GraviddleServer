using TelegramBotNM.Bot;

namespace TelegramBotNM.Notification;

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