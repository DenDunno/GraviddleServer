namespace TelegramBotNM.StateMachineNM.TransitionNM.Condition;

public class InvertCondition : ICondition
{
    private readonly ICondition _condition;

    public InvertCondition(ICondition condition)
    {
        _condition = condition;
    }

    public bool IsTrue()
    {
        return _condition.IsTrue() == false;
    }
}