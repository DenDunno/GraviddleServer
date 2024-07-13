using Domain.Factory;
using Domain.Provider;
using Domain.Repository;
using Domain.Repository.Commands;
using Domain.Repository.Commands.Fetch;
using Domain.Repository.Query;
using TelegramBotTemplate.User;

namespace TelegramBotTemplate.Repository;

public class TelegramUserRepositoryFactory : IFactory<TelegramUsersRepository>
{
    private readonly DatabaseConnection _dbConnection;

    public TelegramUserRepositoryFactory(DatabaseConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public TelegramUsersRepository Create()
    {
        TelegramUserQueries queries = new();
        UserParser parser = new();
        RecordСontainsCommand<long> containsCommand = new(_dbConnection, new QueryBuilder<long>(queries.Contains));
        RecordCommand<TelegramUser, long> addCommand = new(_dbConnection, new QueryBuilder<TelegramUser>(queries.Insert));
        RecordRemoveCommand<long> removeCommand = new(_dbConnection, new QueryBuilder<long>(queries.Remove));
        RecordsDumpCommand<TelegramUser> userDump = new(_dbConnection, parser, new ConstantProvider<string>(queries.GetAll()));
        
        return new TelegramUsersRepository
        {
            Add = new RecordSafeAdd<TelegramUser, long>(containsCommand, addCommand),
            Remove = new RecordSafeRemove<long>(containsCommand, removeCommand),
            UpdateConversation = new RecordCommand<TelegramUser, long>(_dbConnection, new QueryBuilder<TelegramUser>(queries.UpdateConversation)),
            UpdateRole = new RecordCommand<TelegramUser, long>(_dbConnection, new QueryBuilder<TelegramUser>(queries.UpdateRole)),
            Contains = new RecordСontainsCommand<long>(_dbConnection, new QueryBuilder<long>(queries.Contains)),
            Fetch = new RecordFetchCommand<TelegramUser, long>(_dbConnection, parser, new QueryBuilder<long>(queries.Fetch)),
            Dump = userDump,
            AdminsDump = new DumpByRole(userDump, Role.Admin)
        };
    }
}