using Domain.Utils;

namespace TelegramBotTemplate.StateMachineNM.State;

public abstract class States
{
    private readonly List<IState> _all = new();
    
    public IReadOnlyList<IState> All => _all;

    protected void Add(IState state)
    {
        _all.Add(state);
    }

    protected void Add(States states)
    {
        states.All.ForEach(Add);
    }
}