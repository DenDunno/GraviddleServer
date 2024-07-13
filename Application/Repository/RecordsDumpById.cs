using Application.Records;
using Domain.Repository.Commands.Contract;

namespace Application.Repository;

public class RecordsDumpById : IRecordsDump<LevelRecord>
{
    private readonly IRecordsDump<LevelRecord> _dump;
    private readonly string _id;

    public RecordsDumpById(string id, IRecordsDump<LevelRecord> dump)
    {
        _dump = dump;
        _id = id;
    }

    public async Task<List<LevelRecord>> Execute()
    {
        List<LevelRecord> records = (await _dump.Execute());
        
        return records.Where(record => record.Id == _id).ToList();
    }
}