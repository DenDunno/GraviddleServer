
namespace TelegramBotTemplate.StateMachineNM.TransitionNM.Condition;

public interface ICondition
{
    Task<bool> IsTrue();
}