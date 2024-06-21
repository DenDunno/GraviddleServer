using Telegram.Bot;
using TelegramBotNM.Router;

namespace TelegramBotNM.Bot;

public class TelegramBot
{
    private readonly TelegramBotRouter _router;
    private readonly ITelegramBotClient _client;
    public readonly TelegramBotBridge Bridge;
    
    public TelegramBot(ITelegramBotClient client, TelegramBotBridge bridge, IRouterBranch[] branches)
    {
        _router = new TelegramBotRouter(branches);
        _client = client;
        Bridge = bridge;
    }

    public void Run()
    {
        _client.StartReceiving(_router.HandleInput, _router.HandleError);
    }
}