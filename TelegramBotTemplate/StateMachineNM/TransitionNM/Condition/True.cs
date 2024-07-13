namespace TelegramBotTemplate.StateMachineNM.TransitionNM.Condition;

public class True : ICondition
{
    public Task<bool> IsTrue()
    {
        return Task.FromResult(true);
    }
}