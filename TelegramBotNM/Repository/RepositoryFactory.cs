using GraviddleServer.Code.Queries;
using TelegramBotNM.Factory;
using TelegramBotNM.Parser;
using TelegramBotNM.Repository;

namespace GraviddleServer.Code.MsSqlRepositoryNM;

public abstract class RepositoryFactory<TRecord, TKey> : IFactory<Repository<TRecord, TKey>>
{
    protected readonly Queries<TRecord, TKey> Queries;
    protected readonly IRecordParser<TRecord> Parser;
    protected readonly IDatabaseBridge Bridge;

    protected RepositoryFactory(Queries<TRecord, TKey> queries, IDatabaseBridge bridge, IRecordParser<TRecord> parser)
    {
        Queries = queries;
        Bridge = bridge;
        Parser = parser;
    }

    public abstract Repository<TRecord, TKey> Create();
}