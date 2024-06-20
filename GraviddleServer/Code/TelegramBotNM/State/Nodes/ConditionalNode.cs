using Telegram.Bot.Types;

namespace GraviddleServer.Code.TelegramBotNM.State;

public class ConditionalNode : INode
{
    private readonly ICondition _condition;
    private readonly INode _falseNode;
    private readonly INode _trueNode;

    public ConditionalNode(INode trueNode, INode falseNode, ICondition condition)
    {
        _falseNode = falseNode;
        _condition = condition;
        _trueNode = trueNode;
    }

    public Task<INode> Execute(Message input, CancellationToken token)
    {
        return Task.FromResult(_condition.Evaluate(input) ? _trueNode : _falseNode);
    }
}