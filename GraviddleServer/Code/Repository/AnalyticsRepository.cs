using GraviddleServer.Code.Repository.Records;
using TelegramBotNM.Repository.Commands.Contract;

namespace GraviddleServer.Code.Repository;

public class AnalyticsRepository 
{
    public required IRecordsDump<LevelRecord> Dump { get; init; }
    public required IRecordContains<string> Contains { get; init; }
    public required IRecordAdd<LevelRecord, string> Add { get; init; }
    public required IRecordsDump<LevelRecord> GameUsersDump { get; init; }
    public required IRecordFetch<LevelRecord, string> Fetch { get; init; }
}