using GraviddleServer.Code.TelegramBotNM.Commands;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace GraviddleServer.Code.TelegramBotNM.Router;

public class MessageCommandsRouterBranch : RouterBranch<long, string>
{
    public MessageCommandsRouterBranch(IReadOnlyDictionary<string, IBotCommand<long>> commands) : base(commands)
    {
    }

    protected override UpdateType UpdateType => UpdateType.Message;

    protected override long FetchCommandInput(Update update) => update.Message!.Chat.Id;

    protected override string FetchCommandKey(Update update) => update.Message!.Text!;
}