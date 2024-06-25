namespace TelegramBotNM.StateMachineNM.State;

public class RootState : BaseState
{
    public override bool IsPassive => false;

    public RootState() : base("Root")
    {
    }
}