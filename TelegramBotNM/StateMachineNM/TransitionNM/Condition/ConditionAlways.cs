using Telegram.Bot.Types;

namespace TelegramBotNM.StateMachineNM.TransitionNM.Condition;

public class ConditionAlways : ICondition
{
    public bool IsTrue(Message message)
    {
        return true;
    }
}