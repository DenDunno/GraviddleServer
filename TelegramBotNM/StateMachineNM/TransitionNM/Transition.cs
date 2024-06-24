using TelegramBotNM.StateMachineNM.TransitionNM.Condition;

namespace TelegramBotNM.StateMachineNM.TransitionNM;

public class Transition
{
    public readonly ICondition Condition;
    public readonly IState Start;
    public readonly IState End;

    public Transition(IState start, IState end, ICondition condition)
    {
        Condition = condition;
        Start = start;
        End = end;
    }
}