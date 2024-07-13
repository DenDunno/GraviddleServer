using Domain.Repository.Commands.Fetch;

namespace Domain.Repository.Commands.Contract;

public interface IRecordFetch<TRecord, in TKey>
{
    Task<FetchResult<TRecord>> Execute(TKey key);
}