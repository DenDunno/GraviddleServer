using Telegram.Bot.Types;

namespace GraviddleServer.Code.TelegramBotNM.State;

public class BranchNode : INode
{
    private readonly INodeRouter _nodeRouter;

    public BranchNode(INodeRouter nodeRouter)
    {
        _nodeRouter = nodeRouter;
    }

    public async Task<INode> Execute(Message input, CancellationToken token)
    {
        INode node = _nodeRouter.SelectNode(input);
        return await node.Execute(input, token);
    }
}