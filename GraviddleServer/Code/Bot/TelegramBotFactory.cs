using GraviddleServer.Code.Bot.StateMachineNM;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using TelegramBotNM.Bot;
using TelegramBotNM.Commands;
using TelegramBotNM.Router;
using TelegramBotNM.StateMachineNM;
using TelegramBotNM.StateMachineNM.UserProvider;

namespace GraviddleServer.Code.Bot;

public class TelegramBotFactory : ITelegramBotFactory
{
    private readonly Repositories _repositories;
    private readonly string _token;

    public TelegramBotFactory(Repositories repositories, string token)
    {
        _repositories = repositories;
        _token = token;
    }

    public TelegramBot Create()
    {        
        ITelegramBotClient client = new TelegramBotClient(_token);
        TelegramBotBridge botBridge = new(client, _repositories.TelegramUsers.Dump);
        IStateMachineFactory stateMachineFactory = new BotStateMachineFactory(_repositories, botBridge);
        ITelegramUserProvider telegramUserProvider = new TelegramUserProvider(_repositories.TelegramUsers.Fetch);
        
        return new TelegramBot(client, botBridge, new IRouterBranch[]
        {
            new Conversation(_repositories.TelegramUsers, stateMachineFactory, telegramUserProvider),
            new MemberStatusChangedBranch(new Dictionary<ChatMemberStatus, IBotCommand<long>>()
            {
                //{ ChatMemberStatus.Kicked, new RemoveChatCommand(_repositories.TelegramUsers.Remove) }
            })
        });
    }
}