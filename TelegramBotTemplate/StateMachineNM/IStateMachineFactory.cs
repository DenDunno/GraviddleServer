using Telegram.Bot.Types;
using TelegramBotTemplate.User;

namespace TelegramBotTemplate.StateMachineNM;

public interface IStateMachineFactory
{
    public StateMachine Create(Message input, TelegramUser user);
}