using Domain.Logger;
using Telegram.Bot;
using TelegramBotTemplate.Router;

namespace TelegramBotTemplate.Bot;

public class TelegramBot
{
    public readonly TelegramBotBridge Bridge;
    public readonly IMessageLogger Logger;
    private readonly TelegramBotRouter _router;
    private readonly ITelegramBotClient _client;

    public TelegramBot(ITelegramBotClient client, TelegramBotBridge bridge, IMessageLogger logger, IRouterBranch[] branches)
    {
        _router = new TelegramBotRouter(branches, logger);
        _client = client;
        Bridge = bridge;
        Logger = logger;
    }

    public void Run()
    {
        _client.StartReceiving(_router.HandleInput, _router.HandleError);
    }
}