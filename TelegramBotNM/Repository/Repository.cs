using TelegramBotNM.Repository.Commands.Contract;

namespace TelegramBotNM.Repository;

public class Repository<TRecord, TKey>
{
    public IRecordFetch<TRecord, TKey> Fetch { get; init; }
    public IRecordContains<TKey> Contains { get; init; }
    public IRecordUpdate<TRecord> Update { get; init; }
    public IRecordRemove<TKey> Remove { get; init; }
    public IRecordsDump<TRecord> Dump { get; init; }
    public IRecordAdd<TRecord> Add { get; init; }
}