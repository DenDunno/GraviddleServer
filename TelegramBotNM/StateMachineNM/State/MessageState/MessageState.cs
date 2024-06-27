using TelegramBotNM.Bot;

namespace TelegramBotNM.StateMachineNM.State.MessageState;

public class MessageState : BaseState
{
    private readonly TelegramBotBridge _bridge;
    private readonly ITelegramMessage _message;
    protected readonly long ChatId;

    public MessageState(TelegramBotBridge bridge, long chatId, string text)
        : this(bridge, chatId, new PlainText(text))
    {
    }
    
    public MessageState(TelegramBotBridge bridge, long chatId, ITelegramMessage message) 
    {
        _message = message;
        ChatId = chatId;
        _bridge = bridge;
    }

    protected override async Task OnEnter(CancellationToken token)
    {
        await _bridge.SendMessage(_message, ChatId, token);
    }
}