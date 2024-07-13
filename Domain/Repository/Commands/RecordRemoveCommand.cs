using Domain.Repository.Commands.Contract;
using Domain.Repository.Query;

namespace Domain.Repository.Commands;

public class RecordRemoveCommand<TKey> : RecordBaseCommand<TKey>, IRecordRemove<TKey> 
{
    public RecordRemoveCommand(IDatabaseBridge bridge, IQueryBuilder<TKey> queryBuilder) : base(bridge, queryBuilder)
    {
    }

    public async Task Execute(TKey key)
    {
        await Bridge.ExecuteNonQuery(GetQuery(key));
    }
}