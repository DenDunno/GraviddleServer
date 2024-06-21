using GraviddleServer.Code.MsSqlRepositoryNM;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using TelegramBotNM.Bot;
using TelegramBotNM.Commands;
using TelegramBotNM.Commands.MessageCommands.ChatRepositoryCommands;
using TelegramBotNM.Factory;
using TelegramBotNM.Repository;
using TelegramBotNM.Router;
using TelegramBotNM.StateMachineNM;
using TelegramBotNM.StateMachineNM.TransitionNM;
using TelegramBotNM.StateMachineNM.TransitionNM.Condition;
using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.Bot;

public class GraviddleAnalyticsBotFactory : ITelegramBotFactory
{
    private readonly IDatabaseBridge _databaseBridge;
    private readonly string _token;

    public GraviddleAnalyticsBotFactory(IDatabaseBridge databaseBridge, string token)
    {
        _databaseBridge = databaseBridge;
        _token = token;
    }

    public TelegramBot Create()
    {        
        Repository<TelegramUser, long> repository = new UserRepositoryFactory(_databaseBridge).Create();
        TelegramBotClient client = new(_token);
        TelegramBotBridge botBridge = new(client, repository.Dump);

        return new TelegramBot(client, botBridge, new IRouterBranch[]
        {
            new MessageCommandsRouterBranch(BuildStateMachine(repository)),
            new MemberStatusRouterBranch(new Dictionary<ChatMemberStatus, IBotCommand<long>>()
            {
                { ChatMemberStatus.Kicked, new RemoveChatCommand(repository.Remove) }
            })
        });
    }

    private StateMachine BuildStateMachine(Repository<TelegramUser, long> userRepository)
    {
        BotStates states = new(userRepository.Add);
        IReadOnlyDictionary<Type, List<Transition>> transitions = GetTransitions(states);
        StateIdCalculator stateIdCalculator = new(states.All);
        stateIdCalculator.Initialize();
        
        StateMachine botStateMachine = new(transitions, stateIdCalculator, userRepository.Update, userRepository.Fetch);
        return botStateMachine;
    }

    private IReadOnlyDictionary<Type, List<Transition>> GetTransitions(BotStates states)
    {
        TransitionsBuilder builder = new();

        builder.Add(new Transition(states.Root, states.Start, new StringComparisonCondition("/start")));

        return builder.Build();
    }
}