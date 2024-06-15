using Telegram.Bot;
namespace GraviddleServer.TelegramBot;

public class TelegramBotBridge
{
    private readonly TelegramBotClient _client;

    public TelegramBotBridge(TelegramBotClient client)
    {
        _client = client;
    }

    public async Task SendMessage(string text, long chatId, CancellationToken token)
    {
        await _client.SendTextMessageAsync(chatId, text, cancellationToken: token);
    }
}