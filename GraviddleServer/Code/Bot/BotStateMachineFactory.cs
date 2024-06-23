using TelegramBotNM.Bot;
using TelegramBotNM.Repository;
using TelegramBotNM.StateMachineNM;
using TelegramBotNM.StateMachineNM.TransitionNM;
using TelegramBotNM.StateMachineNM.TransitionNM.Condition;
using TelegramBotNM.UserNM;
using StringComparison = TelegramBotNM.StateMachineNM.TransitionNM.Condition.StringComparison;

namespace GraviddleServer.Code.Bot;

public class BotStateMachineFactory : IStateMachineFactory
{
    private readonly Repository<TelegramUser, long> _repository;
    private readonly TelegramBotBridge _botBridge;

    public BotStateMachineFactory(Repository<TelegramUser, long> repository, TelegramBotBridge botBridge)
    {
        _repository = repository;
        _botBridge = botBridge;
    }

    public StateMachine Create(string userInput, TelegramUser user)
    {
        BotStates states = new(user, _repository, _botBridge);
        StateIdCalculator stateIdCalculator = new(states.All);
        stateIdCalculator.Initialize();
        
        TransitionsPresenter transitions = GetTransitions(states, userInput);
        IState initialState = stateIdCalculator.IdToState(user.ConversationState);
        StateMachine stateMachine = new(transitions, initialState, stateIdCalculator);
        
        return stateMachine;
    }

    private TransitionsPresenter GetTransitions(BotStates states, string input)
    {
        TransitionsPresenter transitions = new();
        StringComparison authorizationCondition = new(input, "1223");
        
        transitions.Add(states.Root, states.Start, new StringComparison(input, "/start"));
        transitions.Add(states.Root, states.Authorization, new StringComparison(input, "/authorize"));
        transitions.Add(states.Authorization, states.AdminState, authorizationCondition);
        transitions.Add(states.Authorization, states.InvalidPassword, new InvertCondition(authorizationCondition));
        transitions.Add(states.AdminState, states.Root, new ConditionAlways());
        transitions.Add(states.InvalidPassword, states.Root, new ConditionAlways());

        return transitions;
    }
}