namespace TelegramBotNM.StateMachineNM.TransitionNM.Condition;

public class StringComparison : ICondition 
{
    private readonly string _targetString;
    private readonly string _input;

    public StringComparison(string input, string targetString)
    {
        _targetString = targetString;
        _input = input;
    }

    public bool IsTrue()
    {
        return _input == _targetString;
    }
}