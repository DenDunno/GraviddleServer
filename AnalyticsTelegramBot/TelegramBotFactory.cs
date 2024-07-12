using AnalyticsTelegramBot.StateMachineNM;
using Application;
using Domain.Logger;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using TelegramBotTemplate.Bot;
using TelegramBotTemplate.Router;
using TelegramBotTemplate.Router.Commands;
using TelegramBotTemplate.StateMachineNM;
using TelegramBotTemplate.User;
using TelegramBotTemplate.Utils;

namespace AnalyticsTelegramBot;

public class TelegramBotFactory : ITelegramBotFactory
{
    private readonly Repositories _repositories;
    private readonly SecureData _secureData;

    public TelegramBotFactory(SecureData secureData, Repositories repositories)
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
            new Conversation(_repositories.TelegramUsers.UpdateConversation, stateMachineFactory, telegramUserProvider),
            new MemberStatusChangedBranch(new Dictionary<ChatMemberStatus, IBotCommand<long>>()
            {
                { ChatMemberStatus.Kicked, new StopCommand(_repositories.TelegramUsers.Remove) }
            })
        });
    }
}