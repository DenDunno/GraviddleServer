using GraviddleServer.Code.MsSqlRepositoryNM;
using GraviddleServer.Code.Parser;
using GraviddleServer.Code.Queries;
using Telegram.Bot;
using Telegram.Bot.Types;
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

namespace GraviddleServer.Code.Bot;

public class GraviddleAnalyticsBotFactory : ITelegramBotFactory
{
    private readonly MsSqlDatabaseBridge _databaseBridge;
    private readonly string _token;

    public GraviddleAnalyticsBotFactory(MsSqlDatabaseBridge databaseBridge, string token)
    {
        _databaseBridge = databaseBridge;
        _token = token;
    }

    public TelegramBot Create()
    {
        MsSqlRepositoryFactory repositoryFactory = new(_databaseBridge);
        UserRepository userRepository = repositoryFactory.CreateUserRepository();
        
        TelegramBotClient client = new(_token);
        TelegramBotBridge botBridge = new(client, userRepository.Dump);
        BotStates states = new();
        IReadOnlyDictionary<Type, List<Transition>> transitions = GetTransitions(states);
        StateIdCalculator stateIdCalculator = new(states.All);
        StateMachine botStateMachine = new(transitions, stateIdCalculator, userRepository.Update, userRepository.Fetch);
        
        return new TelegramBot(client, botBridge, new IRouterBranch[]
        {
            new MessageCommandsRouterBranch(botStateMachine),

            new MemberStatusRouterBranch(new Dictionary<ChatMemberStatus, IBotCommand<long>>()
            {
                { ChatMemberStatus.Kicked, new RemoveChatCommand(chatRepository) }
            })
        });
    }

    private IReadOnlyDictionary<Type, List<Transition>> GetTransitions(BotStates states)
    {
        TransitionsBuilder builder = new();
        
        builder.Add(new Transition(states.Root, states.Start, new StringComparisonCondition("/start")));
        builder.Add(new Transition(states.Root, states.Start, new StringComparisonCondition("/start")));
        builder.Add(new Transition(states.Root, states.Start, new StringComparisonCondition("/start")));
        
        return builder.Build();
    }
}