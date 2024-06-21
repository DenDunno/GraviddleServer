using TelegramBotNM.StateMachineNM.TransitionNM.Condition;

namespace TelegramBotNM.StateMachineNM.TransitionNM;

public class Transition
{
    public readonly IState StateFrom;
    public readonly IState StateTo;
    public readonly ICondition Condition;

    public Transition(IState stateFrom, IState stateTo, ICondition condition)
    {
        Condition = condition;
        StateFrom = stateFrom;
        StateTo = stateTo;
    }
}