using TelegramBotNM.Repository.Commands.Contract;
using TelegramBotNM.Repository.Query;

namespace TelegramBotNM.Repository.Commands;

public class RecordRemoveCommand<TKey> : RecordBaseCommand<TKey>, IRecordRemove<TKey> 
{
    public RecordRemoveCommand(IDatabaseBridge bridge, IQueryBuilder<TKey> queryBuilder) : base(bridge, queryBuilder)
    {
    }

    public void Execute(TKey key)
    {
        Bridge.ExecuteNonQuery(GetQuery(key));
    }
}