using GraviddleServer.Code.Bot.Messages;
using GraviddleServer.Code.Parser;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TelegramBotNM.Notification;
using TelegramBotNM.Repository.Commands.Contract;

namespace GraviddleServer.Code.API;

[ApiController]
public class Endpoints : ControllerBase
{
    private readonly IRecordsDump<LevelRecord> _recordsDump;
    private readonly IRecordAdd<LevelRecord, string> _recordAdd;
    private readonly INotification _notification;

    public Endpoints(INotification notification, IRecordAdd<LevelRecord, string> recordAdd, IRecordsDump<LevelRecord> recordsDump)
    {
        _notification = notification;
        _recordsDump = recordsDump;
        _recordAdd = recordAdd;
    }

    public string Greet()
    {
        return "Hey there, what are doing here?";
    }

    public async Task<string> PostLevelResult(string levelRecordJson)
    {
        LevelRecord levelRecord = JsonConvert.DeserializeObject<LevelRecord>(levelRecordJson)!;

        string message = await new RecordMessage(levelRecord).GetText();
        await _notification.Notify(message);
        _recordAdd.Execute(levelRecord);

        return levelRecord.ToString();
    }

    public string GetAllRecords()
    {
        IList<LevelRecord> dump = _recordsDump.Execute();

        return dump.Any() ? dump.Select(x => x.ToString()).Aggregate((s, s1) => s + '\n' + s1) : 
            "No records"; 
    }
}