namespace TelegramBotTemplate.StateMachineNM.TransitionNM.Condition;

public class IsEqual<T> : ICondition 
{
    private readonly T _target;
    private readonly T _input;

    public IsEqual(T input, T target)
    {
        _target = target;
        _input = input;
    }

    public Task<bool> IsTrue()
    {
        return Task.FromResult(EqualityComparer<T>.Default.Equals(_target , _input));
    }
}