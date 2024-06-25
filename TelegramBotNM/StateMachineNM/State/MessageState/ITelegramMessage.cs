using Telegram.Bot.Types.Enums;

namespace TelegramBotNM.StateMachineNM.State.MessageState;

public interface ITelegramMessage
{
    ParseMode? Mode { get; }
    public Task<string> GetText();
}