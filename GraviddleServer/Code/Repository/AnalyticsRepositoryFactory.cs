using GraviddleServer.Code.MsSqlRepositoryNM.QueriesNM;
using GraviddleServer.Code.Parser;
using TelegramBotNM.Factory;
using TelegramBotNM.Repository;
using TelegramBotNM.Repository.Commands;
using TelegramBotNM.Repository.Query;

namespace GraviddleServer.Code.MsSqlRepositoryNM;

public class AnalyticsRepositoryFactory : IFactory<AnalyticsRepository>
{
    private readonly IDatabaseBridge _bridge;

    public AnalyticsRepositoryFactory(IDatabaseBridge bridge)
    {
        _bridge = bridge;
    }
    
    public AnalyticsRepository Create()
    {
        AnalyticsQueries queries = new();
        LevelRecordParser parser = new();
        
        return new AnalyticsRepository
        {
            Add = new RecordCommand<LevelRecord, string>(_bridge, new QueryBuilder<LevelRecord>(queries.Insert)),
            Dump = new RecordsDumpCommand<LevelRecord>(_bridge, parser, new QueryProvider(queries.GetAll()))
        };
    }
}