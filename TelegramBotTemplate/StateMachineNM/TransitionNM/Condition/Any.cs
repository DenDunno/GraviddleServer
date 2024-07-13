namespace TelegramBotTemplate.StateMachineNM.TransitionNM.Condition;

public class Any : ICondition
{
    private readonly IList<ICondition> _conditions;

    public Any(ICondition[] conditions)
    {
        _conditions = conditions;
    }

    public async Task<bool> IsTrue()
    {
        List<Task<bool>> tasks = _conditions.Select(condition => condition.IsTrue()).ToList();
        await Task.WhenAll(tasks);
        
        return tasks.Any(task => task.Result);
    }
}