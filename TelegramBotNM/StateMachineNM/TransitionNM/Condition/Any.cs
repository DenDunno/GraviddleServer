namespace TelegramBotNM.StateMachineNM.TransitionNM.Condition;

public class Any : ICondition
{
    private readonly IList<ICondition> _conditions;

    public Any(params ICondition[] conditions)
    {
        _conditions = conditions;
    }

    public bool IsTrue()
    {
        return _conditions.Any(condition => condition.IsTrue());
    }
}