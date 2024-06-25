
namespace TelegramBotNM.StateMachineNM.State;

public abstract class BaseState : IState
{
    protected BaseState() : this(string.Empty)
    {
    }

    protected BaseState(string name)
    {
        Name = name;
    }

    public string Name { get; }
    public virtual bool IsPassive => true;

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