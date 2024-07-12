using AnalyticsTelegramBot.StateMachineNM.ConditionsNM;
using Telegram.Bot.Types;
using TelegramBotTemplate.Bot;
using TelegramBotTemplate.StateMachineNM;
using TelegramBotTemplate.StateMachineNM.State;
using TelegramBotTemplate.StateMachineNM.TransitionNM;
using TelegramBotTemplate.User;

namespace AnalyticsTelegramBot.StateMachineNM;

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

    public StateMachine Create(Message input, TelegramUser user)
    {
        Conditions conditions = new(user, input.Text!, _adminPassword, _repositories.Analytics);
        BotStates states = new(user, input, _repositories, _botBridge);
        TransitionPresenterFactory transitionsPresenterFactory = new();
        TransitionsPresenter transitions = transitionsPresenterFactory.Create(states, conditions);
        StateIdCalculator stateIdCalculator = new(states.All);
        stateIdCalculator.Initialize();

        IState initialState = stateIdCalculator.IdToState(user.ConversationState);
        StateMachine stateMachine = new(transitions, initialState, stateIdCalculator);

        return stateMachine;
    }
}