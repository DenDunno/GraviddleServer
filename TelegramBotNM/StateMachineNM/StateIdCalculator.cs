namespace TelegramBotNM.StateMachineNM;

public class StateIdCalculator
{
    private readonly Dictionary<Type, int> _ids = new();
    private readonly IList<IState> _states;

    public StateIdCalculator(IList<IState> states)
    {
        _states = states;
    }

    public void Initialize()
    {
        foreach (IState state in _states)
        {
            _ids[state.GetType()] = _ids.Count;
        }
    }

    public int StateToId(IState state)
    {
        return _ids[state.GetType()];
    }

    public IState IdToState(int id)
    {
        return _states[id];
    }
}