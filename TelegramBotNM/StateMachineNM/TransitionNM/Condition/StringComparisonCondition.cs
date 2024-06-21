using Telegram.Bot.Types;

namespace TelegramBotNM.StateMachineNM.TransitionNM.Condition;

public class StringComparisonCondition : ICondition 
{
    private readonly string _targetString;

    public StringComparisonCondition(string targetString)
    {
        _targetString = targetString;
    }

    public bool IsTrue(Message message)
    {
        return message.Text == _targetString;
    }
}