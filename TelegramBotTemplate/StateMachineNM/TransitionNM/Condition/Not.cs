namespace TelegramBotTemplate.StateMachineNM.TransitionNM.Condition;

public class Not : ICondition
{
    private readonly ICondition _condition;

    public Not(ICondition condition)
    {
        _condition = condition;
    }

    public async Task<bool> IsTrue()
    {
        return await _condition.IsTrue() == false;
    }
}