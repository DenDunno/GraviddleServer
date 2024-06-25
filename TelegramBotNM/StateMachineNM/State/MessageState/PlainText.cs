using Telegram.Bot.Types.Enums;

namespace TelegramBotNM.StateMachineNM.State.MessageState;

public class PlainText : ITelegramMessage
{
    private readonly string _text;

    public PlainText(string text)
    {
        _text = text;
    }

    public ParseMode? Mode => null;
    public Task<string> GetText() => Task.FromResult(_text);
}