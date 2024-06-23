using GraviddleServer.Code.MsSqlRepositoryNM.QueriesNM;
using TelegramBotNM.Parser;
using TelegramBotNM.Repository;
using TelegramBotNM.Repository.Commands;
using TelegramBotNM.Repository.Query;
using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.MsSqlRepositoryNM;

public class UserRepositoryFactory : RepositoryFactory<TelegramUser, long>
{
    public UserRepositoryFactory(IDatabaseBridge bridge) : base(new UserQueries(), bridge, new UserParser())
    {
    }

    public override Repository<TelegramUser, long> Create()
    {
        return new Repository<TelegramUser, long>()
        {
            Add = new RecordCommand<TelegramUser>(Bridge, new QueryBuilder<TelegramUser>(Queries.Insert)),
            Remove = new RecordCommand<long>(Bridge, new QueryBuilder<long>(Queries.Remove)),
            Update = new RecordCommand<TelegramUser>(Bridge, new QueryBuilder<TelegramUser>(Queries.Update)),
            Contains = new RecordСontainsCommand<long>(Bridge, new QueryBuilder<long>(Queries.Contains)),
            Fetch = new RecordFetchCommand<TelegramUser, long>(Bridge, Parser, new QueryBuilder<long>(Queries.Fetch)),
            Dump = new RecordsDumpCommand<TelegramUser>(Bridge, Parser, new QueryProvider(Queries.GetAll()))
        };
    }
}