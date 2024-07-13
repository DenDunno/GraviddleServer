using Domain.Repository.Commands.Contract;
using Domain.Repository.Query;

namespace Domain.Repository.Commands;

public class RecordСontainsCommand<TKey> : RecordBaseCommand<TKey>, IRecordContains<TKey>
{
    public RecordСontainsCommand(IDatabaseBridge bridge, IQueryBuilder<TKey> queryBuilder) : base(bridge, queryBuilder)
    {
    }

    public async Task<bool> Execute(TKey key)
    {
        return await Bridge.ExecuteScalar(GetQuery(key)) > 0;
    }
}