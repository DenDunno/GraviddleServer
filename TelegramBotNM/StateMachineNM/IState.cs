
namespace TelegramBotNM.StateMachineNM;

public interface IState
{
    bool IsUnblocking { get; }
    Task Enter(CancellationToken token);
    void Exit();
}