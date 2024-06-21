using Telegram.Bot.Types;

namespace TelegramBotNM.StateMachineNM.TransitionNM.Condition;

public interface ICondition
{
    bool IsTrue(Message message);
}