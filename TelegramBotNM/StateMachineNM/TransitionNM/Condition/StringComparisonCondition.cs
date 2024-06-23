namespace TelegramBotNM.StateMachineNM.TransitionNM.Condition;

public class StringComparisonCondition : ICondition 
{
    private readonly string _targetString;
    private readonly string _input;

    public StringComparisonCondition(string input, string targetString)
    {
        _targetString = targetString;
        _input = input;
    }

    public bool IsTrue()
    {
        return _input == _targetString;
    }
}