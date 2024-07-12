namespace Domain.Repository.Commands.Contract;

public interface IRecordFetch<TRecord, in TKey>
{
    bool TryExecute(TKey key, out TRecord record);
}