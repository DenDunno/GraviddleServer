using Telegram.Bot.Types;

namespace GraviddleServer.Code.TelegramBotNM.State;

public class MessageNode : BaseNode
{
    private readonly TelegramBotBridge _bridge;
    private readonly string _message;

    public MessageNode(TelegramBotBridge bridge, string message) : this(bridge, message, new EmptyNode()) 
    {
    }
    
    public MessageNode(TelegramBotBridge bridge, string message, INode next) : base(next)
    {
        _message = message;
        _bridge = bridge;
    }

    protected override async Task OnExecute(Message input, CancellationToken token)
    {
        await _bridge.SendMessage(_message, input.Chat.Id, token);
    }
}