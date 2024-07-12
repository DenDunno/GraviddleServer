using Domain.Repository.Commands.Contract;
using TelegramBotTemplate.StateMachineNM.TransitionNM.Condition;

namespace AnalyticsTelegramBot.StateMachineNM.ConditionsNM;

public class ValidPlayerIdCondition : ICondition
{
    private readonly IRecordContains<string> _gameUserExists;
    private readonly string _userInput;

    public ValidPlayerIdCondition(string userInput, IRecordContains<string> gameUserExists)
    {
        _gameUserExists = gameUserExists;
        _userInput = userInput;
    }

    public bool IsTrue()
    {
        return _gameUserExists.Execute(_userInput);
    }
}