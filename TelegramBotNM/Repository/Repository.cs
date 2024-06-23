using TelegramBotNM.Repository.Commands.Contract;

namespace TelegramBotNM.Repository;

public class Repository<TRecord, TKey> where TRecord : IDatabaseModel<TKey>
{
    public IRecordFetch<TRecord, TKey> Fetch { get; init; } = null!;
    public IRecordContains<TKey> Contains { get; init; } = null!;
    public IRecordUpdate<TRecord> Update { get; init; } = null!;
    public IRecordRemove<TKey> Remove { get; init; } = null!;
    public IRecordsDump<TRecord> Dump { get; init; } = null!;
    public IRecordAdd<TRecord, TKey> Add { get; init; } = null!;
}