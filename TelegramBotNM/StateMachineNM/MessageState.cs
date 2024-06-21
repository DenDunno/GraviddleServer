using Telegram.Bot.Types;
using TelegramBotNM.Bot;

namespace TelegramBotNM.StateMachineNM;

public class MessageState : BaseState
{
    private readonly TelegramBotBridge _bridge;
    private readonly string _message;

    public MessageState(TelegramBotBridge bridge, string message)
    {
        _message = message;
        _bridge = bridge;
    }

    protected override async Task OnEnter(Message message, CancellationToken token)
    {
        await _bridge.SendMessage(_message, message.Chat.Id, token);
    }
}