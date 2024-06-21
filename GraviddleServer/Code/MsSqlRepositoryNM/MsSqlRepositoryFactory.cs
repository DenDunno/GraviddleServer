using GraviddleServer.Code.Queries;
using TelegramBotNM.Parser;
using TelegramBotNM.Repository;
using TelegramBotNM.Repository.Commands;
using TelegramBotNM.Repository.Query;
using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.MsSqlRepositoryNM;

public class MsSqlRepositoryFactory
{
    private readonly IDatabaseBridge _bridge;

    public MsSqlRepositoryFactory(IDatabaseBridge bridge)
    {
        _bridge = bridge;
    }

    public UserRepository CreateUserRepository()
    {
        UserQueries queries = new();
        UserParser parser = new();
        
        return new UserRepository()
        {
            Add = new RecordCommand<TelegramUser>(_bridge, new QueryBuilder<TelegramUser>(queries.Insert)),
            Remove = new RecordCommand<TelegramUser>(_bridge, new QueryBuilder<TelegramUser>(queries.Remove)),
            Update = new RecordCommand<TelegramUser>(_bridge, new QueryBuilder<TelegramUser>(queries.Update)),
            Contains = new RecordСontainsCommand<long>(_bridge, new QueryBuilder<long>(queries.Contains)),
            Fetch = new RecordFetchCommand<TelegramUser, long>(_bridge, parser, new QueryBuilder<long>(queries.Fetch)),
            Dump = new RecordsDumpCommand<TelegramUser>(_bridge, parser, new QueryProvider(queries.GetAll()))
        };
    }
}