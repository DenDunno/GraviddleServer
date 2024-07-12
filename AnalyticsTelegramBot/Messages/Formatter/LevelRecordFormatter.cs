using AnalyticsTelegramBot.Messages.TableMessages;
using Application.Records;
using Domain.Notification;

namespace AnalyticsTelegramBot.Messages.Formatter;

public class LevelRecordFormatter : IFormatter<LevelRecord>
{
    public string Execute(LevelRecord record)
    {
        Table table = new("Name", record.Name);
        table.Add("Level", record.Level);
        table.Add("Stars", record.Stars);
        table.Add("Time", record.Time);
        table.Add("DeathCount", record.DeathCount);

        return "<b>A player finished a level:</b>\n\n" + table.Build();
    }
}