using GraviddleServer.Code.API;
using GraviddleServer.Code.Bot.Messages;
using TelegramBotNM.Notification;

namespace GraviddleServer.Code.Formatter;

public class LevelRecordFormatter : IFormatter<LevelRecord>
{
    public string Execute(LevelRecord record)
    {
        Table table = new("Name", record.Name);
        table.Add("Level", record.Level);
        table.Add("Stars", record.Stars);
        table.Add("Time", record.Time);
        table.Add("DeathCount", record.DeathCount);

        return table.Build();
    }
}