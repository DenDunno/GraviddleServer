using GraviddleServer.Code.Parser;
using GraviddleServer.Code.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GraviddleServer.Code.API;

[ApiController]
public class Endpoints : ControllerBase
{
    private readonly IRepository<LevelRecord> _levelRecords;
    private readonly INotification _notification;

    public Endpoints(INotification notification, IRepository<LevelRecord> levelRecords)
    {
        _notification = notification;
        _levelRecords = levelRecords;
    }

    public string Greet()
    {
        return "Hey there, what are doing here?";
    }

    public string PostLevelResult(string levelRecordJson)
    {
        LevelRecord levelRecord = JsonConvert.DeserializeObject<LevelRecord>(levelRecordJson)!;
        string levelResultRecord = levelRecord.ToString();

        _notification.Notify(levelResultRecord);
        _levelRecords.Add(levelRecord);

        return levelResultRecord;
    }

    public string GetAllRecords()
    {
        return _levelRecords.GetAll().Select(x => x.ToString()).Aggregate((s, s1) => s + '\n' + s1);
    }
}