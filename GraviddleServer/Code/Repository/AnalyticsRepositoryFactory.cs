using GraviddleServer.Code.Parser;
using GraviddleServer.Code.Repository.Records;
using TelegramBotNM.Repository;
using TelegramBotNM.Repository.Commands;
using TelegramBotNM.Repository.Query;
using TelegramBotNM.Utils;

namespace GraviddleServer.Code.Repository;

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
            Dump = new RecordsDumpCommand<LevelRecord>(_bridge, parser, new QueryProvider(queries.GetAll())),
            Contains = new RecordСontainsCommand<string>(_bridge, new QueryBuilder<string>(queries.Contains)),
            GameUsersDump = new RecordsDumpCommand<LevelRecord>(_bridge, parser, new QueryProvider(queries.GetUsers())),
            Fetch = new RecordFetchCommand<LevelRecord, string>(_bridge, parser, new QueryBuilder<string>(queries.Fetch))
        };
    }
}