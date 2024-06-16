using GraviddleServer.Level;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace GraviddleServer;

[ApiController]
public class Endpoints : ControllerBase
{
    private readonly INotification _notification;

    public Endpoints(INotification notification)
    {
        _notification = notification;
    }

    public string Greet()
    {
        return "Hey there, what are doing here?";
    }

    public string PostLevelResult(string levelResultJson)
    {
        LevelResult levelResult = JsonConvert.DeserializeObject<LevelResult>(levelResultJson)!;
        string levelResultRecord = levelResult.ToString();
        _notification.Notify(levelResultRecord);
        
        return levelResultRecord;
    }

    public string GetAllRecords()
    {
        return "All records";
    }
}