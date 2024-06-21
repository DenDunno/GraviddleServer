using TelegramBotNM.Repository.Commands.Contract;
using TelegramBotNM.Repository.Query;

namespace TelegramBotNM.Repository.Commands;

public class RecordСontainsCommand<TKey> : RecordBaseCommand<TKey>, IRecordContains<TKey>
{
    public RecordСontainsCommand(IDatabaseBridge bridge, IQueryBuilder<TKey> queryBuilder) : base(bridge, queryBuilder)
    {
    }

    public bool Execute(TKey key)
    {
        return Bridge.ExecuteScalar(GetQuery(key)) > 0;
    }
}