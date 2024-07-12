using Telegram.Bot.Types.Enums;
using TelegramBotTemplate.Bot;

namespace TelegramBotTemplate.Notification;

public class TelegramBotTextNotification : TelegramBotNotification<string>
{
    public TelegramBotTextNotification(TelegramBotBridge bridge, ParseMode? parseMode) : base(bridge, parseMode)
    {
    }

    public override async Task Notify(string record)
    {
        await Bridge.SendTextToAll(record, ParseMode);
    }
}