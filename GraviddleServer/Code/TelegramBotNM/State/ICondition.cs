using Telegram.Bot.Types;

namespace GraviddleServer.Code.TelegramBotNM.State;

public interface ICondition
{
    bool Evaluate(Message input);
}