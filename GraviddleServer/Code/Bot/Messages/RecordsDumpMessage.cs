using GraviddleServer.Code.API;
using GraviddleServer.Code.Repository.Records;
using Telegram.Bot.Types.Enums;
using TelegramBotNM.Repository.Commands.Contract;
using TelegramBotNM.StateMachineNM.State.MessageState;

namespace GraviddleServer.Code.Bot.Messages;

public class RecordsDumpMessage : ITelegramMessage
{
    private readonly IRecordsDump<LevelRecord> _levelRecordsDump;

    public RecordsDumpMessage(IRecordsDump<LevelRecord> levelRecordsDump)
    {
        _levelRecordsDump = levelRecordsDump;
    }

    public ParseMode? Mode => ParseMode.Html;
    
    public Task<string> GetText()
    {
        IList<LevelRecord> records = _levelRecordsDump.Execute();
        Table table = new("Num", "Name", "Stars", "Level", "Time", "DeathCount");
        
        int index = 1;
        foreach (LevelRecord record in records)
        {
            table.Add(index++, record.Name, record.Stars, record.Level, record.Time, record.DeathCount);
        }

        return Task.FromResult(table.Build());
    }
}