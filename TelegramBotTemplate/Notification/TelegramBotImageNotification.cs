using Domain.Notification;
using Telegram.Bot.Types.Enums;
using TelegramBotTemplate.Bot;

namespace TelegramBotTemplate.Notification;

public class TelegramBotImageNotification : TelegramBotNotification<ImageMessageData>
{
    public TelegramBotImageNotification(TelegramBotBridge bridge, ParseMode? parseMode) : base(bridge, parseMode)
    {
    }

    public override async Task Notify(ImageMessageData data)
    {
        await Bridge.SendPNGToAll(data, ParseMode);
    }
}