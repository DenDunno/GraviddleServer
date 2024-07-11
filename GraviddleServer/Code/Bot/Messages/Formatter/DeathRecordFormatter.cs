using GraviddleServer.Code.Bot.Messages.TableMessages;
using GraviddleServer.Code.Repository.Records;
using TelegramBotNM.Notification;
using TelegramBotNM.Utils;

namespace GraviddleServer.Code.Bot.Messages.Formatter;

public class DeathRecordFormatter : IFormatter<DeathRecord>
{
    public string Execute(DeathRecord record)
    {
        return $"<b>Player <u>{record.Name}</u> died {record.Reasons.Length} times" +
               $" at level <u>{record.Level}</u>.</b>" +
               $"\n\n{GetReasonsList(record)}";
    }

    private string GetReasonsList(DeathRecord record)
    {
        Table table = new("Num", "Reason");

        int index = 1;
        record.Reasons.ForEach(reason => table.Add(index++, reason));

        return table.Build();
    }
}