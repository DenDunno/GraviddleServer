using TelegramBotNM.Factory;
using TelegramBotNM.Parser;

namespace TelegramBotNM.Repository;

public abstract class RepositoryFactory<TRecord, TKey> : IFactory<Repository<TRecord, TKey>>  where TRecord : IDatabaseModel<TKey>
{
    protected readonly IRecordParser<TRecord> Parser;
    protected readonly IDatabaseBridge Bridge;

    protected RepositoryFactory(IDatabaseBridge bridge, IRecordParser<TRecord> parser)
    {
        Bridge = bridge;
        Parser = parser;
    }

    public abstract Repository<TRecord, TKey> Create();
}