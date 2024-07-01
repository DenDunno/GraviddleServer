using GraviddleServer.Code.API;
using GraviddleServer.Code.Bot.StateMachineNM;
using GraviddleServer.Code.Repository;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using TelegramBotNM.Bot;
using TelegramBotNM.Logger;
using TelegramBotNM.Router;
using TelegramBotNM.Router.Commands;
using TelegramBotNM.StateMachineNM;
using TelegramBotNM.User;

namespace GraviddleServer.Code.Bot;

public class TelegramBotFactory : ITelegramBotFactory
{
    private readonly Repositories _repositories;
    private readonly SecureData _secureData;

    public TelegramBotFactory(Repositories repositories, SecureData secureData)
    {
        _repositories = repositories;
        _secureData = secureData;
    }

    public TelegramBot Create()
    {
        ITelegramBotClient client = new TelegramBotClient(_secureData.TelegramBotToken);
        TelegramBotBridge botBridge = new(client, _repositories.TelegramUsers.Dump);
        IStateMachineFactory stateMachineFactory = new BotStateMachineFactory(_repositories, botBridge, _secureData.AdminPassword);
        ITelegramUserProvider telegramUserProvider = new TelegramUserProvider(_repositories.TelegramUsers.Fetch);
        IMessageLogger messageLogger = new BotAdminMessageLogger(botBridge, _repositories.TelegramUsers.AdminsDump);
        
        return new TelegramBot(client, botBridge, messageLogger, new IRouterBranch[]
        {
            new Conversation(_repositories.TelegramUsers, stateMachineFactory, telegramUserProvider),
            new MemberStatusChangedBranch(new Dictionary<ChatMemberStatus, IBotCommand<long>>()
            {
                { ChatMemberStatus.Kicked, new StopCommand(_repositories.TelegramUsers.Remove) }
            })
        });
    }
}