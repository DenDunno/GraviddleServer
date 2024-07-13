using Application.Records;
using Application.Repository.Parser;
using Domain.Factory;
using Domain.Provider;
using Domain.Repository;
using Domain.Repository.Commands;
using Domain.Repository.Commands.Fetch;
using Domain.Repository.Query;

namespace Application.Repository;

public class AnalyticsRepositoryFactory : IFactory<AnalyticsRepository>
{
    private readonly DatabaseConnection _dbConnection;

    public AnalyticsRepositoryFactory(DatabaseConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    
    public AnalyticsRepository Create()
    {
        AnalyticsQueries queries = new();
        LevelRecordParser parser = new();
        
        return new AnalyticsRepository
        {
            Add = new RecordCommand<LevelRecord, string>(_dbConnection, new QueryBuilder<LevelRecord>(queries.Insert)),
            Dump = new RecordsDumpCommand<LevelRecord>(_dbConnection, parser, new ConstantProvider<string>(queries.GetAll())),
            Contains = new RecordСontainsCommand<string>(_dbConnection, new QueryBuilder<string>(queries.Contains)),
            GameUsersDump = new RecordsDumpCommand<LevelRecord>(_dbConnection, parser, new ConstantProvider<string>(queries.GetUsers())),
            Fetch = new RecordFetchCommand<LevelRecord, string>(_dbConnection, parser, new QueryBuilder<string>(queries.Fetch))
        };
    }
}