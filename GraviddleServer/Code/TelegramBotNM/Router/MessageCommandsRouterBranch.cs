using GraviddleServer.Code.TelegramBotNM.Commands;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace GraviddleServer.Code.TelegramBotNM.Router;

public class MessageCommandsRouterBranch : RouterBranch<Message, string>
{
    public MessageCommandsRouterBranch(IReadOnlyDictionary<string, IBotCommand<Message>> commands) : base(commands)
    {
    }

    protected override UpdateType UpdateType => UpdateType.Message;

    protected override Message FetchCommandInput(Update update) => update.Message!;

    protected override string FetchCommandKey(Update update) => update.Message!.Text!;
}