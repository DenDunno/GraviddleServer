using Telegram.Bot.Types.Enums;
using TelegramBotNM.Bot;
using TelegramBotNM.Repository.Commands.Contract;

namespace GraviddleServer.Code.Logger;

public class BotAdminLogger : ILogger
{
    private readonly IRecordsDump<long> _admins;
    private readonly TelegramBotBridge _bridge;

    public BotAdminLogger(TelegramBotBridge bridge, IRecordsDump<long> admins)
    {
        _bridge = bridge;
        _admins = admins;
    }

    public async Task Log(string text)
    {
        text = $"<b><u> Exception: {text}</u></b>";
        await _bridge.Send(text, _admins.Execute(), ParseMode.Html);
    }
}