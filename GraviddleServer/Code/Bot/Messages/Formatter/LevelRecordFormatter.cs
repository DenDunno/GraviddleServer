using GraviddleServer.Code.Bot.Messages.TableMessages;
using GraviddleServer.Code.Repository.Records;
using TelegramBotNM.Notification;

namespace GraviddleServer.Code.Bot.Messages.Formatter;

public class LevelRecordFormatter : IFormatter<LevelRecord>
{
    public string Execute(LevelRecord record)
    {
        Table table = new("Name", record.Name);
        table.Add("Id", record.Id);
        table.Add("Level", record.Level);
        table.Add("Stars", record.Stars);
        table.Add("Time", record.Time);
        table.Add("DeathCount", record.DeathCount);

        return "<b>A player finished a level:</b>\n\n" + table.Build();
    }
}