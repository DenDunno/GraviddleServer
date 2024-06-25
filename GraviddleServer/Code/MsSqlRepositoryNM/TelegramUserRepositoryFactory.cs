using GraviddleServer.Code.MsSqlRepositoryNM.QueriesNM;
using TelegramBotNM.Factory;
using TelegramBotNM.Parser;
using TelegramBotNM.Repository;
using TelegramBotNM.Repository.Commands;
using TelegramBotNM.Repository.Query;
using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.MsSqlRepositoryNM;

public class TelegramUserRepositoryFactory : IFactory<TelegramUsersRepository>
{
    private readonly IDatabaseBridge _bridge;

    public TelegramUserRepositoryFactory(IDatabaseBridge bridge)
    {
        _bridge = bridge;
    }

    public TelegramUsersRepository Create()
    {
        UserQueries queries = new();
        UserParser parser = new();
        RecordСontainsCommand<long> containsCommand = new(_bridge, new QueryBuilder<long>(queries.Contains));
        RecordCommand<TelegramUser, long> addCommand = new(_bridge, new QueryBuilder<TelegramUser>(queries.Insert));
        RecordRemoveCommand<long> removeCommand = new(_bridge, new QueryBuilder<long>(queries.Remove));
        
        return new TelegramUsersRepository
        {
            Add = new RecordSafeAdd<TelegramUser, long>(containsCommand, addCommand),
            Remove = new RecordSafeRemove<long>(containsCommand, removeCommand),
            UpdateConversation = new RecordCommand<TelegramUser, long>(_bridge, new QueryBuilder<TelegramUser>(queries.UpdateConversation)),
            UpdateRole = new RecordCommand<TelegramUser, long>(_bridge, new QueryBuilder<TelegramUser>(queries.UpdateRole)),
            Contains = new RecordСontainsCommand<long>(_bridge, new QueryBuilder<long>(queries.Contains)),
            Fetch = new RecordFetchCommand<TelegramUser, long>(_bridge, parser, new QueryBuilder<long>(queries.Fetch)),
            Dump = new RecordsDumpCommand<TelegramUser>(_bridge, parser, new QueryProvider(queries.GetAll()))
        };
    }
}