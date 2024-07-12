using Application.Records;
using Application.Repository.Parser;
using Domain.Factory;
using Domain.Repository;
using Domain.Repository.Commands;
using Domain.Repository.Query;

namespace Application.Repository;

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