using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotNM.Router;

public class TelegramBotRouter
{
    private readonly IEnumerable<IRouterBranch> _routerBranches;

    public TelegramBotRouter(IEnumerable<IRouterBranch> routerBranches)
    {
        _routerBranches = routerBranches;
    }

    public async Task HandleInput(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        foreach (IRouterBranch routerBranch in _routerBranches)
        {
            await routerBranch.Handle(update, cancellationToken);
        }
    }

    public Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}