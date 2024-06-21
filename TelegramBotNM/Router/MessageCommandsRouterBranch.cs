using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotNM.StateMachineNM;

namespace TelegramBotNM.Router;

public class MessageCommandsRouterBranch : IRouterBranch
{
    private readonly StateMachine _stateMachine;

    public MessageCommandsRouterBranch(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    
    public async Task Handle(Update update, CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.Message)
        {
            await _stateMachine.Handle(update.Message!, cancellationToken);
        }
    }
}