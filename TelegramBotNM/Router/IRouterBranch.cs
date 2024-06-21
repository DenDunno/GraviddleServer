using Telegram.Bot.Types;

namespace TelegramBotNM.Router;

public interface IRouterBranch
{
    Task Handle(Update update, CancellationToken cancellationToken);
}