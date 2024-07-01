using TelegramBotNM.User;

namespace TelegramBotNM.StateMachineNM;

public interface IStateMachineFactory
{
    public StateMachine Create(string userInput, TelegramUser user);
}