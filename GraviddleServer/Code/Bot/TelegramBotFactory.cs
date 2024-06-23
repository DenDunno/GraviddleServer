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
using TelegramBotNM.UserNM;

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
        Repository<TelegramUser, long> repository = new UserRepositoryFactory(_databaseBridge).Create();
        ITelegramBotClient client = new TelegramBotClient(_token);
        TelegramBotBridge botBridge = new(client, repository.Dump);
        IStateMachineFactory stateMachineFactory = new BotStateMachineFactory(repository, botBridge);
        IUserProvider userProvider = new UserProvider(repository.Fetch);
        
        return new TelegramBot(client, botBridge, new IRouterBranch[]
        {
            new Conversation(repository, stateMachineFactory, userProvider),
            new MemberStatusChangedBranch(new Dictionary<ChatMemberStatus, IBotCommand<long>>()
            {
                { ChatMemberStatus.Kicked, new RemoveChatCommand(repository.Remove) }
            })
        });
    }
}