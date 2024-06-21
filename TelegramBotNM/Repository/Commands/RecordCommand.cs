using TelegramBotNM.Repository.Commands.Contract;
using TelegramBotNM.Repository.Query;

namespace TelegramBotNM.Repository.Commands;

public class RecordCommand<TRecord> : RecordBaseCommand<TRecord>, 
    IRecordAdd<TRecord>, IRecordRemove<TRecord>, IRecordUpdate<TRecord>
{
    public RecordCommand(IDatabaseBridge bridge, IQueryBuilder<TRecord> queryBuilder) : base(bridge, queryBuilder)
    {
    }

    public void Execute(TRecord element)
    {
        Bridge.ExecuteNonQuery(GetQuery(element));
    }
}