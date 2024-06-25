
namespace TelegramBotNM.StateMachineNM.State;

public interface IState
{    
    string Name { get; }
    bool IsPassive { get; }
    Task Enter(CancellationToken token);
    void Exit();
}