using Telegram.Bot.Types.Enums;

namespace TelegramBotTemplate.StateMachineNM.State.MessageState;

public interface ITelegramMessage
{
    ParseMode? Mode { get; }
    public Task<string> GetText();
}