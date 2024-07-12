namespace TelegramBotTemplate.StateMachineNM.State;

public class EmptyState : BaseState
{
    public EmptyState(string debugName, bool isPassive = true) : base(debugName)
    {
        IsPassive = isPassive;
    }
}