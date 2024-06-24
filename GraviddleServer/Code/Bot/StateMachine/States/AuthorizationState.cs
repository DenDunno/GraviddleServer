using TelegramBotNM.Bot;
using TelegramBotNM.StateMachineNM;

namespace GraviddleServer.Code.Bot;

public class AuthorizationState : MessageState
{
    public override bool IsPassive => false;

    public AuthorizationState(TelegramBotBridge bridge, long chatId) : base(bridge, chatId, "Enter password:")
    {
    }
}