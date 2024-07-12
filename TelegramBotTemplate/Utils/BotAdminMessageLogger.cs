using Domain.Logger;
using Domain.Repository.Commands.Contract;
using TelegramBotTemplate.Bot;

namespace TelegramBotTemplate.Utils;

public class BotAdminMessageLogger : IMessageLogger
{
    private readonly IRecordsDump<long> _admins;
    private readonly TelegramBotBridge _bridge;

    public BotAdminMessageLogger(TelegramBotBridge bridge, IRecordsDump<long> admins)
    {
        _bridge = bridge;
        _admins = admins;
    }

    public async Task Log(string text)
    {
        await _bridge.SendText(text, _admins.Execute());
    }
}