using Telegram.Bot.Types;

namespace GraviddleServer.Code.TelegramBotNM.Router;

public interface IRouterBranch
{
    Task Handle(Update update, CancellationToken cancellationToken);
}