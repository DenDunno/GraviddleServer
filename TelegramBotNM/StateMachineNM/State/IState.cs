
namespace TelegramBotNM.StateMachineNM;

public interface IState
{    
    bool IsPassive { get; }
    Task Enter(CancellationToken token);
    void Exit();
}