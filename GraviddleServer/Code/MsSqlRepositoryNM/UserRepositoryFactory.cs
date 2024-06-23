using GraviddleServer.Code.MsSqlRepositoryNM.QueriesNM;
using TelegramBotNM.Parser;
using TelegramBotNM.Repository;
using TelegramBotNM.Repository.Commands;
using TelegramBotNM.Repository.Query;
using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.MsSqlRepositoryNM;

public class UserRepositoryFactory : RepositoryFactory<TelegramUser, long>
{
    public UserRepositoryFactory(IDatabaseBridge bridge) : base(bridge, new UserParser())
    {
    }

    public override Repository<TelegramUser, long> Create()
    {
        UserQueries queries = new();
        RecordСontainsCommand<long> containsCommand = new(Bridge, new QueryBuilder<long>(queries.Contains));
        RecordCommand<TelegramUser, long> addCommand = new(Bridge, new QueryBuilder<TelegramUser>(queries.Insert));
        RecordRemoveCommand<long> removeCommand = new(Bridge, new QueryBuilder<long>(queries.Remove));
        
        return new Repository<TelegramUser, long>()
        {
            Add = new RecordSafeAdd<TelegramUser, long>(containsCommand, addCommand),
            Remove = new RecordSafeRemove<long>(containsCommand, removeCommand),
            Update = new RecordCommand<TelegramUser, long>(Bridge, new QueryBuilder<TelegramUser>(queries.UpdateConversation)),
            Contains = new RecordСontainsCommand<long>(Bridge, new QueryBuilder<long>(queries.Contains)),
            Fetch = new RecordFetchCommand<TelegramUser, long>(Bridge, Parser, new QueryBuilder<long>(queries.Fetch)),
            Dump = new RecordsDumpCommand<TelegramUser>(Bridge, Parser, new QueryProvider(queries.GetAll()))
        };
    }
}