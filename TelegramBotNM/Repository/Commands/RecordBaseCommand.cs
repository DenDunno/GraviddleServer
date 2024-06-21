using TelegramBotNM.Repository.Query;

namespace TelegramBotNM.Repository.Commands;

public abstract class RecordBaseCommand<T>
{
    protected readonly IDatabaseBridge Bridge;
    private readonly IQueryBuilder<T> _queryBuilder;
    
    protected RecordBaseCommand(IDatabaseBridge bridge, IQueryBuilder<T> queryBuilder)
    {
        _queryBuilder = queryBuilder;
        Bridge = bridge;
    }

    protected string GetQuery(T input) => _queryBuilder.Build(input);
}