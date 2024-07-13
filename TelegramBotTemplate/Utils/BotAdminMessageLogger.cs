using Domain.Logger;
using Domain.Repository.Commands.Contract;
using TelegramBotTemplate.Bot;

namespace TelegramBotTemplate.Utils;

public class BotAdminMessageLogger : IMessageLogger
{
    private readonly IRecordsDump<long> _adminsDump;
    private readonly TelegramBotBridge _bridge;

    public BotAdminMessageLogger(TelegramBotBridge bridge, IRecordsDump<long> adminsDump)
    {
        _bridge = bridge;
        _adminsDump = adminsDump;
    }

    public async Task Log(string text)
    {
        List<long> admins = await _adminsDump.Execute();
        await _bridge.SendText(text, admins);
    }
}