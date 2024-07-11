using GraviddleServer.Code.Bot.StateMachineNM.ConditionsNM;
using GraviddleServer.Code.Repository;
using TelegramBotNM.Bot;
using TelegramBotNM.StateMachineNM;
using TelegramBotNM.StateMachineNM.State;
using TelegramBotNM.StateMachineNM.TransitionNM;
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
        Conditions conditions = new(user, userInput, _adminPassword, _repositories);
        BotStates states = new(user, userInput, _repositories, _botBridge);
        TransitionPresenterFactory transitionsPresenterFactory = new();
        TransitionsPresenter transitions = transitionsPresenterFactory.Create(states, conditions);
        StateIdCalculator stateIdCalculator = new(states.All);
        stateIdCalculator.Initialize();

        IState initialState = stateIdCalculator.IdToState(user.ConversationState);
        StateMachine stateMachine = new(transitions, initialState, stateIdCalculator);
        
        return stateMachine;
    }
}