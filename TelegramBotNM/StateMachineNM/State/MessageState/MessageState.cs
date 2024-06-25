using TelegramBotNM.Bot;

namespace TelegramBotNM.StateMachineNM.State.MessageState;

public class MessageState : BaseState
{
    private readonly TelegramBotBridge _bridge;
    private readonly ITelegramMessage _message;
    private readonly long _chatId;

    public MessageState(TelegramBotBridge bridge, long chatId, string text)
        : this(bridge, chatId, new PlainText(text))
    {
    }
    
    public MessageState(TelegramBotBridge bridge, long chatId, ITelegramMessage message) 
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