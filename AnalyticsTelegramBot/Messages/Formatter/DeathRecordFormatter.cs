using AnalyticsTelegramBot.Messages.TableMessages;
using Application.Records;
using Domain.Notification;
using Domain.Utils;

namespace AnalyticsTelegramBot.Messages.Formatter;

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