using Telegram.Bot.Types;

namespace TelegramBotNM.StateMachineNM;

public abstract class BaseState : IState
{
    public async Task Enter(Message message, CancellationToken token)
    {
        await OnEnter(message, token);
    }

    public void Exit()
    {
        OnExit();
    }

    protected virtual async Task OnEnter(Message message, CancellationToken token)
    {
        await Task.Yield();
    }

    protected virtual void OnExit()
    {
    }
}