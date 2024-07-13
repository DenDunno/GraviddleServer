using Domain.Repository.Commands.Contract;
using Domain.Repository.Query;

namespace Domain.Repository.Commands;

public class RecordCommand<TRecord, TKey> : RecordBaseCommand<TRecord>, 
    IRecordAdd<TRecord, TKey>, 
    IRecordUpdate<TRecord> 
    where TRecord : IDatabaseModel<TKey>
{
    public RecordCommand(DatabaseConnection connection, IQueryBuilder<TRecord> queryBuilder) : base(connection, queryBuilder)
    {
    }

    public async Task Execute(TRecord element)
    {
        using IDatabaseBridge bridge = Connection.Open();
        await bridge.ExecuteNonQuery(GetQuery(element));
    }
}