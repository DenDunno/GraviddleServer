using Domain.Notification;
using Telegram.Bot.Types.Enums;
using TelegramBotTemplate.Bot;

namespace TelegramBotTemplate.Notification;

public abstract class TelegramBotNotification<TInput> : INotification<TInput>
{
    protected readonly TelegramBotBridge Bridge;
    protected readonly ParseMode? ParseMode;

    protected TelegramBotNotification(TelegramBotBridge bridge, ParseMode? parseMode)
    {
        ParseMode = parseMode;
        Bridge = bridge;
    }

    public abstract Task Notify(TInput data);
}