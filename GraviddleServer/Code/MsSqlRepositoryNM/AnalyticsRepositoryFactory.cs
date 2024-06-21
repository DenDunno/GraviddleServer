using GraviddleServer.Code.Parser;
using GraviddleServer.Code.Queries;
using TelegramBotNM.Repository;
using TelegramBotNM.Repository.Commands;
using TelegramBotNM.Repository.Query;

namespace GraviddleServer.Code.MsSqlRepositoryNM;

public class AnalyticsRepositoryFactory : RepositoryFactory<LevelRecord, string>
{
    public AnalyticsRepositoryFactory(IDatabaseBridge bridge) :
        base(new AnalyticsQueries(), bridge, new LevelRecordParser())
    {
    }
    
    public override Repository<LevelRecord, string> Create()
    {
        return new Repository<LevelRecord, string>()
        {
            Add = new RecordCommand<LevelRecord>(Bridge, new QueryBuilder<LevelRecord>(Queries.Insert)),
            Dump = new RecordsDumpCommand<LevelRecord>(Bridge, Parser, new QueryProvider(Queries.GetAll()))
        };
    }
}