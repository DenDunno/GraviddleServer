using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace GraviddleServer.TelegramBot;

public class TelegramBotRouter
{
    private readonly TelegramBotBridge _bridge;

    public TelegramBotRouter(TelegramBotBridge bridge)
    {
        _bridge = bridge;
    }

    public async Task HandleInput(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.Message)
        {
            await _bridge.SendMessage($"You said = {update.Message!.Text}", update.Message.Chat.Id, cancellationToken);
        }
    }

    public Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}