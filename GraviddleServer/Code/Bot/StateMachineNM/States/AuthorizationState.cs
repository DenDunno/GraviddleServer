using TelegramBotNM.StateMachineNM.State;

namespace GraviddleServer.Code.Bot.StateMachineNM.States;

public class AuthorizationState : BaseState
{
    public override bool IsPassive => false;

    public AuthorizationState() : base("Authorization")
    {
    }
}