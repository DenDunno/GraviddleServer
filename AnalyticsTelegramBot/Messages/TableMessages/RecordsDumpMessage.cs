using Application.Records;
using Domain.Repository.Commands.Contract;

namespace AnalyticsTelegramBot.Messages.TableMessages;

public class RecordsDumpMessage : TableMessage
{
    private readonly IRecordsDump<LevelRecord> _levelRecordsDump;

    public RecordsDumpMessage(IRecordsDump<LevelRecord> levelRecordsDump)
    {
        _levelRecordsDump = levelRecordsDump;
    }

    protected override string[] Columns => new[] { "Num", "Name", "Stars", "Level", "Time", "DeathCount" };

    protected override async Task WriteRaws(List<object[]> raws)
    {
        IList<LevelRecord> records = await _levelRecordsDump.Execute();

        for (int i = 0; i < records.Count; ++i)
        {
            LevelRecord record = records[i];
            raws.Add(new object[]
                { i + 1, record.Name, record.Stars, record.Level, record.Time, record.DeathCount });
        }
    }
}