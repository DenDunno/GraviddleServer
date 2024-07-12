using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBotTemplate.Router;

public abstract class RouterBranch<T> : IRouterBranch
{
    public async Task Handle(Update update, CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType)
        {
            T input = GetKey(update);
            await OnHandle(input, cancellationToken);
        }
    }

    protected abstract UpdateType UpdateType { get; }
    protected abstract T GetKey(Update update);
    protected abstract Task OnHandle(T message, CancellationToken cancellationToken);
}