using GraviddleServer.Code.MsSqlRepositoryNM;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using TelegramBotNM.Bot;
using TelegramBotNM.Commands;
using TelegramBotNM.Commands.MessageCommands.ChatRepositoryCommands;
using TelegramBotNM.Repository;
using TelegramBotNM.Router;
using TelegramBotNM.StateMachineNM;
using TelegramBotNM.StateMachineNM.UserProvider;

namespace GraviddleServer.Code.Bot;

public class TelegramBotFactory : ITelegramBotFactory
{
    private readonly IDatabaseBridge _databaseBridge;
    private readonly string _token;

    public TelegramBotFactory(IDatabaseBridge databaseBridge, string token)
    {
        _databaseBridge = databaseBridge;
        _token = token;
    }

    public TelegramBot Create()
    {        
        UserRepository userRepository = new UserRepositoryFactory(_databaseBridge).Create();
        ITelegramBotClient client = new TelegramBotClient(_token);
        TelegramBotBridge botBridge = new(client, userRepository.Dump);
        IStateMachineFactory stateMachineFactory = new BotStateMachineFactory(userRepository, botBridge);
        IUserProvider userProvider = new UserProvider(userRepository.Fetch);
        
        return new TelegramBot(client, botBridge, new IRouterBranch[]
        {
            new Conversation(userRepository, stateMachineFactory, userProvider),
            new MemberStatusChangedBranch(new Dictionary<ChatMemberStatus, IBotCommand<long>>()
            {
                { ChatMemberStatus.Kicked, new RemoveChatCommand(userRepository.Remove) }
            })
        });
    }
}