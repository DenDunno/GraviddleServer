using Telegram.Bot.Types.Enums;

namespace TelegramBotNM.StateMachineNM;

public interface ITelegramMessage
{
    ParseMode ParseMode { get; }
    public string GetText();
}