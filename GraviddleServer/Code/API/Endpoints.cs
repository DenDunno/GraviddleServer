using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TelegramBotNM.Notification;
using TelegramBotNM.Repository.Commands.Contract;

namespace GraviddleServer.Code.API;

public class Endpoints
{
    private readonly INotification<LevelRecord> _levelRecordNotification;
    private readonly INotification<DeathRecord> _deathRecordNotification;
    private readonly IRecordAdd<LevelRecord, string> _recordAdd;
    private readonly IRecordsDump<LevelRecord> _recordsDump;

    public Endpoints(IRecordAdd<LevelRecord, string> recordAdd, IRecordsDump<LevelRecord> recordsDump,
        INotification<LevelRecord> levelRecordNotification, INotification<DeathRecord> deathRecordNotification)
    {
        _levelRecordNotification = levelRecordNotification;
        _deathRecordNotification = deathRecordNotification;
        _recordsDump = recordsDump;
        _recordAdd = recordAdd;
    }

    [HttpGet]
    public string Greet()
    {
        return "Hey there, what are doing here?";
    }

    [HttpGet]
    public string GetAllRecords()
    {
        IList<LevelRecord> dump = _recordsDump.Execute();
        return JsonConvert.SerializeObject(dump);
    }

    [HttpPost]
    public async Task<IResult> PostLevelRecord([FromBody] LevelRecord levelRecord)
    {
        await _levelRecordNotification.Notify(levelRecord);
        _recordAdd.Execute(levelRecord);

        return Results.Ok(levelRecord);
    }

    [HttpPost]
    public async Task<IResult> PostDeathRecord([FromBody] DeathRecord deathRecord)
    {
        await _deathRecordNotification.Notify(deathRecord);
        return Results.Ok(deathRecord);
    }
}