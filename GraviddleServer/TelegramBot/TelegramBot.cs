using GraviddleServer.ChatRepository;
using GraviddleServer.TelegramBot.Commands;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace GraviddleServer.TelegramBot;

public class TelegramBot
{
    public readonly TelegramBotBridge Bridge;
    private readonly TelegramBotRouter _router;
    private readonly ITelegramBotClient _client;

    public TelegramBot(string token, IChatsRepository chatsRepository)
    {
        _client = new TelegramBotClient(token);
        Bridge = new TelegramBotBridge(_client, chatsRepository);
        _router = new TelegramBotRouter(new IRouterBranch[]
        {
            new MessageCommandsRouterBranch(new Dictionary<string, IBotCommand<long>>()
            {
                { MessageCommands.Start, new AddChatCommand(chatsRepository) },
                { MessageCommands.Stop, new RemoveChatCommand(chatsRepository) },
                { MessageCommands.ChatsDump, new GetChatsDumpCommand(chatsRepository, Bridge) },
            }),

            new MemberStatusRouterBranch(new Dictionary<ChatMemberStatus, IBotCommand<long>>()
            {
                { ChatMemberStatus.Kicked, new RemoveChatCommand(chatsRepository) }
            })
        });
    }

    public void Run()
    {
        _client.StartReceiving(_router.HandleInput, _router.HandleError);
    }
}