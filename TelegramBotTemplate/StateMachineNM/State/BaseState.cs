
namespace TelegramBotTemplate.StateMachineNM.State;

public abstract class BaseState : IState
{
    protected BaseState() : this(string.Empty)
    {
    }

    protected BaseState(string debugName)
    {
        DebugName = debugName;
    }

    public string DebugName { get; }
    public bool IsPassive { get; protected init; } = true;

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