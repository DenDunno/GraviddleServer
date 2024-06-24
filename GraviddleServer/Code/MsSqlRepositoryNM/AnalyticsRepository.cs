using GraviddleServer.Code.Parser;
using TelegramBotNM.Repository.Commands.Contract;

namespace GraviddleServer.Code.MsSqlRepositoryNM;

public class AnalyticsRepository 
{
    public required IRecordAdd<LevelRecord, string> Add { get; init; }
    public required IRecordsDump<LevelRecord> Dump { get; init; }
}