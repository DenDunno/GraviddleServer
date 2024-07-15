using Application.Records;
using Domain.Notification;
using Domain.Repository.Commands.Contract;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GraviddleServer.Code;

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
        return "Hey there, what are you doing here?";
    }

    [HttpGet]
    [Route("/all")]
    public async Task<string> GetAllRecords()
    {
        IList<LevelRecord> dump = await _recordsDump.Execute();
        return JsonConvert.SerializeObject(dump);
    }

    [HttpPost]
    [Route("/postLevelRecord")]
    public async Task<IResult> PostLevelRecord([FromBody] LevelRecord levelRecord)
    {
        await _levelRecordNotification.Notify(levelRecord);
        await _recordAdd.Execute(levelRecord);

        return Results.Ok(levelRecord);
    }

    [HttpPost]
    [Route("/postDeathRecord")]
    public async Task<IResult> PostDeathRecord([FromBody] DeathRecord deathRecord)
    {
        await _deathRecordNotification.Notify(deathRecord);
        return Results.Ok(deathRecord);
    }
}