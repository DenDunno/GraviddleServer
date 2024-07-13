using Domain.Repository.Commands.Contract;
using Domain.Repository.Query;

namespace Domain.Repository.Commands;

public class RecordСontainsCommand<TKey> : RecordBaseCommand<TKey>, IRecordContains<TKey>
{
    public RecordСontainsCommand(DatabaseConnection connection, IQueryBuilder<TKey> queryBuilder) : base(connection, queryBuilder)
    {
    }

    public async Task<bool> Execute(TKey key)
    {
        using IDatabaseBridge bridge = Connection.Open();
        return await bridge.ExecuteScalar(GetQuery(key)) > 0;
    }
}