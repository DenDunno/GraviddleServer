namespace TelegramBotNM.StateMachineNM.TransitionNM.Condition;

public class ConditionAlways : ICondition
{
    public bool IsTrue()
    {
        return true;
    }
}