using GraviddleServer.ChatRepository;
using GraviddleServer.Code.TelegramBot.Commands;
using GraviddleServer.Code.TelegramBot.Commands.MessageCommands;
using GraviddleServer.Code.TelegramBot.Commands.MessageCommands.ChatRepositoryCommands;
using GraviddleServer.Code.TelegramBot.Router;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace GraviddleServer.Code.TelegramBot;

public class TelegramBot
{
    public readonly TelegramBotBridge Bridge;
    private readonly TelegramBotRouter _router;
    private readonly ITelegramBotClient _client;

    public TelegramBot(string token, IChatsRepository repository)
    {
        _client = new TelegramBotClient(token);
        Bridge = new TelegramBotBridge(_client, repository);
        _router = new TelegramBotRouter(new IRouterBranch[]
        {
            new MessageCommandsRouterBranch(new Dictionary<string, IBotCommand<long>>()
            {
                { MessageCommands.Start, new AddChatCommand(repository) },
                { MessageCommands.Stop, new RemoveChatCommand(repository) },
                { MessageCommands.ChatsDump, new GetChatsDumpCommand(repository, Bridge) },
            }),

            new MemberStatusRouterBranch(new Dictionary<ChatMemberStatus, IBotCommand<long>>()
            {
                { ChatMemberStatus.Kicked, new RemoveChatCommand(repository) }
            })
        });
    }

    public void Run()
    {
        _client.StartReceiving(_router.HandleInput, _router.HandleError);
    }
}