using Telegram.Bot.Types;

namespace TelegramBotNM.StateMachineNM;

public interface IState
{
    Task Enter(Message message, CancellationToken token);
    void Exit();
}