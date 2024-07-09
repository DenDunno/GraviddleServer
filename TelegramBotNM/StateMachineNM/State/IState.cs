
namespace TelegramBotNM.StateMachineNM.State;

public interface IState
{    
    string DebugName { get; }
    bool IsPassive { get; }
    Task Enter(CancellationToken token);
    void Exit();
}