
namespace TelegramBotNM.StateMachineNM;

public interface IState
{
    Task Enter(CancellationToken token);
    void Exit();
}