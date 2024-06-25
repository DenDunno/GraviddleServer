using Telegram.Bot.Types.Enums;
using TelegramBotNM.Bot;

namespace TelegramBotNM.Notification;

public class TelegramBotNotification : INotification
{
    private readonly TelegramBotBridge _bridge;
    private readonly ParseMode? _parseMode;
    
    public TelegramBotNotification(TelegramBotBridge bridge, ParseMode? parseMode = null)
    {
        _parseMode = parseMode;
        _bridge = bridge;
    }

    public async Task Notify(string text)
    {
        await _bridge.SendToAll(text, _parseMode);
    }
}