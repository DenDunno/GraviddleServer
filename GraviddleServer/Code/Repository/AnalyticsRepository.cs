using GraviddleServer.Code.API;
using TelegramBotNM.Repository.Commands.Contract;

namespace GraviddleServer.Code.Repository;

public class AnalyticsRepository 
{
    public required IRecordAdd<LevelRecord, string> Add { get; init; }
    public required IRecordsDump<LevelRecord> Dump { get; init; }
}