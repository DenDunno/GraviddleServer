using TelegramBotTemplate.User;

namespace TelegramBotTemplate.StateMachineNM;

public interface IStateMachineFactory
{
    public StateMachine Create(string userInput, TelegramUser user);
}