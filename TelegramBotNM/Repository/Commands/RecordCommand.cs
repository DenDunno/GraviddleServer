using TelegramBotNM.Repository.Commands.Contract;
using TelegramBotNM.Repository.Query;

namespace TelegramBotNM.Repository.Commands;

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