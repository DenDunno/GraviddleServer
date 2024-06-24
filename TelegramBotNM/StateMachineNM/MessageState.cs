using TelegramBotNM.Bot;

namespace TelegramBotNM.StateMachineNM;

public class MessageState : BaseState
{
    private readonly TelegramBotBridge _bridge;
    private readonly string _message;
    private readonly long _chatId;

    public MessageState(TelegramBotBridge bridge, long chatId, string message)
    {
        _message = message;
        _chatId = chatId;
        _bridge = bridge;
    }

    protected override async Task OnEnter(CancellationToken token)
    {
        await _bridge.SendMessage(_message, _chatId, token);
    }
}