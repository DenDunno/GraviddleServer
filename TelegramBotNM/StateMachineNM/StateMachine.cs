using TelegramBotNM.StateMachineNM.State;
using TelegramBotNM.StateMachineNM.TransitionNM;

namespace TelegramBotNM.StateMachineNM;

public class StateMachine
{
    private readonly TransitionsPresenter _transitionsPresenter;
    private readonly StateIdCalculator _stateIdCalculator;
    private IState _state;

    public StateMachine(TransitionsPresenter transitionsPresenter, IState state, StateIdCalculator stateIdCalculator)
    {
        _transitionsPresenter = transitionsPresenter;
        _stateIdCalculator = stateIdCalculator;
        _state = state;
    }

    public async Task<ConversationResult> UpdateConversation(CancellationToken token)
    {
        bool continueTraversing;

        do
        {
            continueTraversing = await TryTransit(token);
            
        } while (_state.IsPassive && continueTraversing);

        return new ConversationResult(_state.IsPassive == false, _stateIdCalculator.StateToId(_state));
    }

    private async Task<bool> TryTransit(CancellationToken token)
    {
        IState newState = _transitionsPresenter.Transit(_state);
        bool successfulTransition = newState != _state;

        if (successfulTransition)
        {
            _state.Exit();
            _state = newState;
            await _state.Enter(token);
        }

        return successfulTransition;
    }
}