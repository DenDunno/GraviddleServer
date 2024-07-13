using Application.Repository;
using Domain.Repository.Commands.Contract;
using Domain.Repository.Query;

namespace Domain.Repository.Commands;

public class RecordRemoveCommand<TKey> : RecordBaseCommand<TKey>, IRecordRemove<TKey> 
{
    public RecordRemoveCommand(DatabaseConnection connection, IQueryBuilder<TKey> queryBuilder) : base(connection, queryBuilder)
    {
    }

    public async Task Execute(TKey key)
    {
        using IDatabaseBridge bridge = Connection.Open();
        await bridge.ExecuteNonQuery(GetQuery(key));
    }
}