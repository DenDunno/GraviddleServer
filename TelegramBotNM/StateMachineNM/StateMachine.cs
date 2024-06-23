using TelegramBotNM.StateMachineNM.TransitionNM;

namespace TelegramBotNM.StateMachineNM;

public class StateMachine 
{
    private readonly IReadOnlyDictionary<Type, List<Transition>> _transitions;
    private readonly StateIdCalculator _stateIdCalculator;
    private IState _state;

    public StateMachine(IReadOnlyDictionary<Type, List<Transition>> transitions, IState state, StateIdCalculator stateIdCalculator)
    {
        _stateIdCalculator = stateIdCalculator;
        _transitions = transitions;
        _state = state;
    }

    public async Task<ConversationResult> UpdateConversation(CancellationToken token)
    {
        bool isNotDeadEnd = true;
        IState initialState = _state;
        
        while (isNotDeadEnd)
        {
            isNotDeadEnd = await TryTransit(token);
        }

        return new ConversationResult(initialState != _state, _stateIdCalculator.StateToId(_state));
    }

    private async Task<bool> TryTransit(CancellationToken token)
    {
        foreach (Transition transition in _transitions[_state.GetType()])
        {
            if (transition.Condition.IsTrue())
            {
                _state.Exit();
                _state = transition.StateTo;
                await _state.Enter(token);
                return true;
            }
        }

        return false;
    }
}