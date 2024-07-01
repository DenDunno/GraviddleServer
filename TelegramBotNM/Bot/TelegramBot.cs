using Telegram.Bot;
using TelegramBotNM.Logger;
using TelegramBotNM.Router;

namespace TelegramBotNM.Bot;

public class TelegramBot
{
    private readonly TelegramBotRouter _router;
    private readonly ITelegramBotClient _client;
    public readonly TelegramBotBridge Bridge;
    public readonly IMessageLogger Logger;
    
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