using Telegram.Bot.Types.Enums;

namespace TelegramBotTemplate.StateMachineNM.State.MessageState;

public class PlainText : ITelegramMessage
{
    private readonly string _text;

    public PlainText(string text)
    {
        _text = text;
    }

    public ParseMode? Mode => ParseMode.Html;
    public Task<string> GetText() => Task.FromResult(_text);
}