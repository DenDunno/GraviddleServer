using TelegramBotNM.StateMachineNM.State;

namespace TelegramBotNM.StateMachineNM;

public class StateIdCalculator
{
    private readonly Dictionary<IState, int> _ids = new();
    private readonly IReadOnlyList<IState> _states;

    public StateIdCalculator(IReadOnlyList<IState> states)
    {
        _states = states;
    }

    public void Initialize()
    {
        foreach (IState state in _states)
        {
            _ids[state] = _ids.Count;
        }
    }
    
    public int StateToId(IState state)
    {
        return _ids[state];
    }

    public IState IdToState(int id)
    {
        return _states[id];
    }
}