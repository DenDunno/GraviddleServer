using Telegram.Bot.Types;

namespace TelegramBotTemplate.Router;

public interface IRouterBranch
{
    Task Handle(Update update, CancellationToken cancellationToken);
}