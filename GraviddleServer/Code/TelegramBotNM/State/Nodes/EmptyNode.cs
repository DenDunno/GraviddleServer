using Telegram.Bot.Types;

namespace GraviddleServer.Code.TelegramBotNM.State;

public class EmptyNode : INode
{
    public Task<INode> Execute(Message input, CancellationToken token)
    {
        return Task.FromResult<INode>(this);
    }
}