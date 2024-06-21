using Telegram.Bot.Types;
using TelegramBotNM.Bot;

namespace TelegramBotNM.Commands.MessageCommands;

public abstract class OutputMessageCommand : IMessageCommand
{
    private readonly TelegramBotBridge _bridge;

    protected OutputMessageCommand(TelegramBotBridge bridge)
    {
        _bridge = bridge;
    }

    public async Task Handle(Message message, CancellationToken token)
    {
        string output = await EvaluateOutput();
        await _bridge.SendMessage(output, message.Chat.Id, token);
    }

    protected abstract Task<string> EvaluateOutput();
}