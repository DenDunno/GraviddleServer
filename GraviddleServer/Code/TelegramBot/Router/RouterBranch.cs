using GraviddleServer.Code.TelegramBot.Commands;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace GraviddleServer.Code.TelegramBot.Router;

public abstract class RouterBranch<TCommandInput, TCommandKey> : IRouterBranch
{
    private readonly IReadOnlyDictionary<TCommandKey, IBotCommand<TCommandInput>> _commands;

    protected RouterBranch(IReadOnlyDictionary<TCommandKey, IBotCommand<TCommandInput>> commands)
    {
        _commands = commands;
    }

    public async Task Handle(Update update, CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType)
        {
            TCommandInput commandInput = FetchCommandInput(update);
            TCommandKey commandKey = FetchCommandKey(update);

            if (_commands.ContainsKey(commandKey))
            {
                await _commands[commandKey].Handle(commandInput, cancellationToken);
            }
        }
    }
    
    protected abstract UpdateType UpdateType { get; }
    protected abstract TCommandInput FetchCommandInput(Update update);
    protected abstract TCommandKey FetchCommandKey(Update update);
}