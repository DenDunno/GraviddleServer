using GraviddleServer.Code.Repository;
using TelegramBotNM.Bot;
using TelegramBotNM.StateMachineNM;
using TelegramBotNM.StateMachineNM.State;
using TelegramBotNM.StateMachineNM.TransitionNM;
using TelegramBotNM.StateMachineNM.TransitionNM.Condition;
using TelegramBotNM.User;

namespace GraviddleServer.Code.Bot.StateMachineNM;

public class BotStateMachineFactory : IStateMachineFactory
{
    private readonly TelegramBotBridge _botBridge;
    private readonly Repositories _repositories;
    private readonly string _adminPassword;

    public BotStateMachineFactory(Repositories repositories, TelegramBotBridge botBridge, string adminPassword)
    {
        _adminPassword = adminPassword;
        _repositories = repositories;
        _botBridge = botBridge;
    }

    public StateMachine Create(string userInput, TelegramUser user)
    {
        Conditions conditions = new(user, userInput, _adminPassword);
        BotStates states = new(user, _repositories, _botBridge);
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

        ConnectActiveStatesToCommandsListener(transitions, states, conditions.AnyCommandEntered);
        transitions.Add(states.CommandsListener, states.Start, conditions.Start);
        transitions.Add(states.CommandsListener, states.Stop, conditions.Stop);
        transitions.Add(states.CommandsListener, states.Admin, conditions.IsAdmin);
        transitions.Add(states.Admin, states.YouAreAlreadyAdmin, conditions.Authorize);
        transitions.Add(states.Admin, states.TelegramUsersDump, conditions.TelegramUsersDump);
        transitions.Add(states.Admin, states.RecordsDump, conditions.RecordsDump);
        transitions.Add(states.CommandsListener, states.User, conditions.IsUser);
        transitions.Add(states.User, states.YouAreNotAdmin, conditions.RestrictedCommand);
        transitions.Add(states.User, states.EnterPassword, conditions.Authorize);
        transitions.Add(states.EnterPassword, states.Authorization, new True());
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

    private void ConnectActiveStatesToCommandsListener(TransitionsPresenter transitions, BotStates states, ICondition anyCommandEntered)
    {
        foreach (IState state in states.All)
        {
            if (state.IsPassive == false)
            {
                transitions.Add(state, states.CommandsListener, anyCommandEntered);
            }
        }
    }
}