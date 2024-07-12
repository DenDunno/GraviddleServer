using Application.Records;
using Domain.Repository.Commands.Contract;

namespace Application.Repository;

public class AnalyticsRepository 
{
    public required IRecordsDump<LevelRecord> Dump { get; init; }
    public required IRecordContains<string> Contains { get; init; }
    public required IRecordAdd<LevelRecord, string> Add { get; init; }
    public required IRecordsDump<LevelRecord> GameUsersDump { get; init; }
    public required IRecordFetch<LevelRecord, string> Fetch { get; init; }
}