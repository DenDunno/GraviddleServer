using TelegramBotNM.UserNM;

namespace TelegramBotNM.StateMachineNM;

public interface IStateMachineFactory
{
    public StateMachine Create(string userInput, TelegramUser user);
}