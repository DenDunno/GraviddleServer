namespace TelegramBotNM.StateMachineNM.TransitionNM;

public class TransitionsBuilder
{
    private readonly List<Transition> _transitions = new();
    
    public void Add(Transition transition)
    {
        _transitions.Add(transition);
    }

    public IReadOnlyDictionary<Type, List<Transition>> Build()
    {
        Dictionary<Type, List<Transition>> result = new();

        foreach (Transition transition in _transitions)
        {
            Type stateFrom = transition.StateFrom.GetType();

            if (result.ContainsKey(stateFrom) == false)
            {
                result.Add(stateFrom, new List<Transition>());
            }
            
            result[stateFrom].Add(transition);
        }

        return result;
    }
}