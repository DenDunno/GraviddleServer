using GraviddleServer.Code.API;
using GraviddleServer.Code.Repository.Records;
using TelegramBotNM.Repository.Commands.Contract;

namespace GraviddleServer.Code.Repository;

public class AnalyticsRepository 
{
    public required IRecordAdd<LevelRecord, string> Add { get; init; }
    public required IRecordsDump<LevelRecord> Dump { get; init; }
    public required IRecordContains<string> Contains { get; init; }
}