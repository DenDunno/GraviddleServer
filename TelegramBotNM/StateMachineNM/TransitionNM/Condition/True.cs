namespace TelegramBotNM.StateMachineNM.TransitionNM.Condition;

public class True : ICondition
{
    public bool IsTrue()
    {
        return true;
    }
}