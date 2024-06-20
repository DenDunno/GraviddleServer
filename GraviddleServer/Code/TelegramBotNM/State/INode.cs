using Telegram.Bot.Types;

namespace GraviddleServer.Code.TelegramBotNM.State;

public interface INode
{
    Task<INode> Execute(Message input, CancellationToken token);
}