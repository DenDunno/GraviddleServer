using Telegram.Bot.Types;

namespace GraviddleServer.Code.TelegramBotNM.State;

public interface INodeRouter
{
    INode SelectNode(Message message);
}