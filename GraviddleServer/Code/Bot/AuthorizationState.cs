using TelegramBotNM.Bot;
using TelegramBotNM.StateMachineNM;

namespace GraviddleServer.Code.Bot;

public class AuthorizationState : MessageState
{
    public override bool IsUnblocking => false;

    public AuthorizationState(TelegramBotBridge bridge, long chatId) : base(bridge, "Enter password:", chatId)
    {
    }
}