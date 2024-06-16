using GraviddleServer.Level;
using Microsoft.AspNetCore.Mvc;
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

    public string PostLevelResult(LevelResult levelResult)
    {
        string recordText = levelResult.ToString();
        _notification.Notify(recordText);

        return recordText;
    }

    public string GetAllRecords()
    {
        return "All records";
    }
}