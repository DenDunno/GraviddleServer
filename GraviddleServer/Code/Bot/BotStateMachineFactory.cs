using TelegramBotNM.Repository;
using TelegramBotNM.StateMachineNM;
using TelegramBotNM.StateMachineNM.TransitionNM;
using TelegramBotNM.StateMachineNM.TransitionNM.Condition;
using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.Bot;

public class BotStateMachineFactory : IStateMachineFactory
{
    private readonly Repository<TelegramUser, long> _repository;

    public BotStateMachineFactory(Repository<TelegramUser, long> repository)
    {
        _repository = repository;
    }

    public StateMachine Create(string userInput, TelegramUser user)
    {
        BotStates states = new(user, _repository);
        StateIdCalculator stateIdCalculator = new(states.All);
        stateIdCalculator.Initialize();
        
        IReadOnlyDictionary<Type, List<Transition>> transitions = GetTransitions(states, userInput);
        IState initialState = stateIdCalculator.IdToState(user.ConversationState);
        StateMachine stateMachine = new(transitions, initialState, stateIdCalculator);
        
        return stateMachine;
    }

    private IReadOnlyDictionary<Type, List<Transition>> GetTransitions(BotStates states, string input)
    {
        TransitionsBuilder builder = new();

        builder.Add(new Transition(states.Root, states.Start, new StringComparisonCondition(input, "/start")));

        return builder.Build();
    }
}