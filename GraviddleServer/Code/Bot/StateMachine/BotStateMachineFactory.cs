using TelegramBotNM.Bot;
using TelegramBotNM.Repository;
using TelegramBotNM.StateMachineNM;
using TelegramBotNM.StateMachineNM.TransitionNM;
using TelegramBotNM.StateMachineNM.TransitionNM.Condition;
using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.Bot;

public class BotStateMachineFactory : IStateMachineFactory
{
    private readonly UserRepository _userRepository;
    private readonly TelegramBotBridge _botBridge;

    public BotStateMachineFactory(UserRepository userRepository, TelegramBotBridge botBridge)
    {
        _userRepository = userRepository;
        _botBridge = botBridge;
    }

    public StateMachine Create(string userInput, TelegramUser user)
    {
        Conditions conditions = new(user, userInput);
        BotStates states = new(user, _userRepository, _botBridge);
        StateIdCalculator stateIdCalculator = new(states.All);
        stateIdCalculator.Initialize();

        TransitionsPresenter transitions = GetTransitions(states, conditions);
        IState initialState = stateIdCalculator.IdToState(user.ConversationState);
        StateMachine stateMachine = new(transitions, initialState, stateIdCalculator);
        
        return stateMachine;
    }

    private TransitionsPresenter GetTransitions(BotStates states, Conditions conditions)
    {
        TransitionsPresenter transitions = new();
        
        transitions.Add(states.Root, states.Start, conditions.Start);
        transitions.Add(states.Root, states.Stop, conditions.Stop);
        transitions.Add(states.Root, states.Admin, conditions.IsAdmin);
        transitions.Add(states.Admin, states.YouAreAlreadyAdmin, conditions.Authorize);
        transitions.Add(states.Admin, states.ChatsDump, conditions.ChatsDump);
        transitions.Add(states.Admin, states.RecordsDump, conditions.RecordsDump);
        transitions.Add(states.Root, states.User, conditions.IsUser);
        transitions.Add(states.User, states.YouAreNotAdmin, conditions.RestrictedCommand);
        transitions.Add(states.User, states.Authorization, conditions.Authorize);
        transitions.Add(states.Authorization, states.SetAdminRole, conditions.ValidAdminPassword);
        transitions.Add(states.Authorization, states.InvalidPassword, conditions.BadAdminPassword);
        transitions.Add(states.InvalidPassword, states.Authorization, new True());
        ConnectDeadEndsToRoot(transitions, states);
        
        return transitions;
    }

    private void ConnectDeadEndsToRoot(TransitionsPresenter transitions, BotStates states)
    {
        foreach (IState state in states.All)
        {
            if (transitions.IsDeadEnd(state))
            {
                transitions.Add(state, states.Root, new True());
            }
        }
    }
}