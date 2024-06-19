using GraviddleServer.Code.API;

namespace GraviddleServer.Code.TelegramBotNM;

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