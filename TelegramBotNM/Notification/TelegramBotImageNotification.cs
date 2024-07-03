using GraviddleServer.Code.API;
using Telegram.Bot.Types.Enums;
using TelegramBotNM.Bot;

namespace TelegramBotNM.Notification;

public class TelegramBotImageNotification : TelegramBotNotification<ImageMessageData>
{
    public TelegramBotImageNotification(TelegramBotBridge bridge, ParseMode? parseMode) : base(bridge, parseMode)
    {
    }

    public override async Task Notify(ImageMessageData data)
    {
        await Bridge.SendImageMessageToAll(data, ParseMode);
    }
}