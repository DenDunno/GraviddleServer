using Telegram.Bot.Types.Enums;
using TelegramBotTemplate.StateMachineNM.State.MessageState;

namespace AnalyticsTelegramBot.Messages.TableMessages;

public abstract class TableMessage : ITelegramMessage
{
    public ParseMode? Mode => ParseMode.Html;

    protected abstract string[] Columns { get; }

    public async Task<string> GetText()
    {
        Table table = new(Columns);
        List<object[]> raws = new();
        await WriteRaws(raws);
        
        foreach (object[] raw in raws)
        {
            table.Add(raw);
        }
        
        return table.Build();
    }

    protected abstract Task WriteRaws(List<object[]> raws);
}