using GraviddleServer.Code.MsSqlRepositoryNM.QueriesNM;
using GraviddleServer.Code.Parser;
using TelegramBotNM.Repository;
using TelegramBotNM.Repository.Commands;
using TelegramBotNM.Repository.Query;

namespace GraviddleServer.Code.MsSqlRepositoryNM;

public class AnalyticsRepositoryFactory : RepositoryFactory<LevelRecord, string>
{
    public AnalyticsRepositoryFactory(IDatabaseBridge bridge) :
        base(bridge, new LevelRecordParser())
    {
    }
    
    public override Repository<LevelRecord, string> Create()
    {
        AnalyticsQueries queries = new();
        
        return new Repository<LevelRecord, string>()
        {
            Add = new RecordCommand<LevelRecord, string>(Bridge, new QueryBuilder<LevelRecord>(queries.Insert)),
            Dump = new RecordsDumpCommand<LevelRecord>(Bridge, Parser, new QueryProvider(queries.GetAll()))
        };
    }
}