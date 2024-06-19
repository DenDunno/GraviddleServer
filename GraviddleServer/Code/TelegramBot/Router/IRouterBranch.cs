using Telegram.Bot.Types;

namespace GraviddleServer.Code.TelegramBot.Router;

public interface IRouterBranch
{
    Task Handle(Update update, CancellationToken cancellationToken);
}