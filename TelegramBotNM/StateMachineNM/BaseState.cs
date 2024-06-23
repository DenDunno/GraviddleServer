
namespace TelegramBotNM.StateMachineNM;

public abstract class BaseState : IState
{
    public async Task Enter(CancellationToken token)
    {
        await OnEnter(token);
    }

    public void Exit()
    {
        OnExit();
    }

    protected virtual async Task OnEnter(CancellationToken token)
    {
        await Task.Yield();
    }

    protected virtual void OnExit()
    {
    }
}