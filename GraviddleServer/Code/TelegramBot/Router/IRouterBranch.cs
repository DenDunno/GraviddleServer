using Telegram.Bot.Types;
namespace GraviddleServer.TelegramBot;

public interface IRouterBranch
{
    Task Handle(Update update, CancellationToken cancellationToken);
}