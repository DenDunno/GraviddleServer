using GraviddleServer.Code.API;
using GraviddleServer.Code.Repository.Records;
using Telegram.Bot.Types.Enums;
using TelegramBotNM.StateMachineNM.State.MessageState;

namespace GraviddleServer.Code.Bot.Messages;

public class RecordMessage : ITelegramMessage
{
    private readonly LevelRecord _record;

    public RecordMessage(LevelRecord record)
    {
        _record = record;
    }

    public ParseMode? Mode => ParseMode.Html;
    
    public Task<string> GetText()
    {
        Table table = new("Name", "Stars", "Level", "Time", "DeathCount");
        table.Add(_record.Name, _record.Stars, _record.Level, _record.Time, _record.DeathCount);

        return Task.FromResult(table.Build());
    }
}