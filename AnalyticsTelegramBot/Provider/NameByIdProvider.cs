using Application.Records;
using Domain.Repository.Commands.Contract;

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

    public string Provide()
    {
        if (_nameFetch.TryExecute(_id, out LevelRecord record) == false)
        {
            throw new Exception($"No such record with id = {_id}");
        };
        
        return record.Name;
    }
}