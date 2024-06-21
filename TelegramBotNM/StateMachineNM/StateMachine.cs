using Telegram.Bot.Types;
using TelegramBotNM.Commands;
using TelegramBotNM.Repository;
using TelegramBotNM.Repository.Commands.Contract;
using TelegramBotNM.StateMachineNM.TransitionNM;
using TelegramBotNM.UserNM;

namespace TelegramBotNM.StateMachineNM;

public class StateMachine : IBotCommand<Message>
{
    private readonly IReadOnlyDictionary<Type, List<Transition>> _transitions;
    private readonly IRecordFetch<TelegramUser, long> _userFetch;
    private readonly IRecordUpdate<TelegramUser> _userUpdate;
    private readonly StateIdCalculator _stateIdCalculator;

    public StateMachine(IReadOnlyDictionary<Type, List<Transition>> transitions, StateIdCalculator stateIdCalculator,
        IRecordUpdate<TelegramUser> userUpdate, IRecordFetch<TelegramUser, long> userFetch)
    {
        _stateIdCalculator = stateIdCalculator;
        _transitions = transitions;
        _userUpdate = userUpdate;
        _userFetch = userFetch;
    }

    public async Task Handle(Message input, CancellationToken token)
    {
        if (_userFetch.TryExecute(input.Chat.Id, out TelegramUser? user) == false)
        {
            user = new TelegramUser(input.Chat.Id, Role.User, 0);
        }
        
        int conversationId = await UpdateConversation(input, token, user);
        _userUpdate.Execute(user with { ConversationState = conversationId });
    }

    private async Task<int> UpdateConversation(Message input, CancellationToken token, TelegramUser user)
    {
        int lastConversationId = user.ConversationState;
        bool isNotDeadEnd = true;
        
        Console.WriteLine($"Start with {_stateIdCalculator.IdToState(lastConversationId).GetType().Name}");
        
        while (isNotDeadEnd)
        {
            int newConversationId = await TryTransit(input, token, lastConversationId);
            Console.WriteLine($"New state {_stateIdCalculator.IdToState(newConversationId).GetType().Name}");
            isNotDeadEnd = lastConversationId != newConversationId;
            lastConversationId = newConversationId;
        }

        return lastConversationId;
    }

    private async Task<int> TryTransit(Message message, CancellationToken token, int conversationStateId)
    {
        IState state = _stateIdCalculator.IdToState(conversationStateId);

        foreach (Transition transition in _transitions[state.GetType()])
        {
            if (transition.Condition.IsTrue(message))
            {
                state.Exit();
                state = transition.StateTo;
                await state.Enter(message, token);
            }
        }

        return _stateIdCalculator.StateToId(state);
    }
}