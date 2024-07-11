using GraviddleServer.Code.Repository.Records;
using TelegramBotNM.Repository.Commands.Contract;

namespace GraviddleServer.Code.Repository;

public class RecordsDumpById : IRecordsDump<LevelRecord>
{
    private readonly IRecordsDump<LevelRecord> _dump;
    private readonly string _id;

    public RecordsDumpById(string id, IRecordsDump<LevelRecord> dump)
    {
        _dump = dump;
        _id = id;
    }

    public List<LevelRecord> Execute()
    {
        return _dump.Execute().Where(record => record.Id == _id).ToList();
    }
}