using Telegram.Bot.Types.Enums;

namespace TelegramBotNM.StateMachineNM;

public class PlainText : ITelegramMessage
{
    private readonly string _text;

    public PlainText(string text)
    {
        _text = text;
    }

    public ParseMode ParseMode => default;
    public string GetText() => _text;
}