using Domain.Repository.Commands.Contract;
using Domain.Repository.Query;

namespace Domain.Repository.Commands;

public class RecordCommand<TRecord, TKey> : RecordBaseCommand<TRecord>, 
    IRecordAdd<TRecord, TKey>, 
    IRecordUpdate<TRecord> 
    where TRecord : IDatabaseModel<TKey>
{
    public RecordCommand(IDatabaseBridge bridge, IQueryBuilder<TRecord> queryBuilder) : base(bridge, queryBuilder)
    {
    }

    public void Execute(TRecord element)
    {
        Bridge.ExecuteNonQuery(GetQuery(element));
    }
}