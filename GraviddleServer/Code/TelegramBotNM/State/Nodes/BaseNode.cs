using Telegram.Bot.Types;

namespace GraviddleServer.Code.TelegramBotNM.State;

public abstract class BaseNode : INode
{
    private readonly INode _nextNode;

    protected BaseNode(INode nextNode)
    {
        _nextNode = nextNode;
    }

    public async Task<INode> Execute(Message input, CancellationToken token)
    {
        await OnExecute(input, token);
        
        return _nextNode;
    }

    protected abstract Task OnExecute(Message input, CancellationToken token);
}