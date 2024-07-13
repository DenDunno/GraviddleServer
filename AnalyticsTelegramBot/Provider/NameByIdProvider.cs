using Application.Records;
using Domain.Provider;
using Domain.Repository.Commands.Contract;
using Domain.Repository.Commands.Fetch;

namespace AnalyticsTelegramBot.Provider;

public class NameByIdProvider : IProvider<string>
{
    private readonly IRecordFetch<LevelRecord, string> _nameFetch;
    private readonly string _id;

    public NameByIdProvider(string id, IRecordFetch<LevelRecord, string> nameFetch)
    {
        _nameFetch = nameFetch;
        _id = id;
    }

    public async Task<string> Provide()
    {
        FetchResult<LevelRecord> result = await _nameFetch.Execute(_id);

        if (result.Success)
        {
            return result.Record.Name;
        }

        throw new Exception($"No such record with id = {_id}");
    }
}