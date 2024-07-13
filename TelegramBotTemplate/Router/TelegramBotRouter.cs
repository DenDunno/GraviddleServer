using Domain.Logger;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotTemplate.Router;

public class TelegramBotRouter
{
    private readonly IEnumerable<IRouterBranch> _routerBranches;
    private readonly IMessageLogger _messageLogger;

    public TelegramBotRouter(IEnumerable<IRouterBranch> routerBranches, IMessageLogger messageLogger)
    {
        _routerBranches = routerBranches;
        _messageLogger = messageLogger;
    }

    public async Task HandleInput(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            foreach (IRouterBranch routerBranch in _routerBranches)
            {
                await routerBranch.Handle(update, cancellationToken);
            }
        }
        catch (Exception exception)
        {
            await _messageLogger.Log(exception.ToString());
        }
    }

    public async Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        await _messageLogger.Log(exception.ToString());
    }
}