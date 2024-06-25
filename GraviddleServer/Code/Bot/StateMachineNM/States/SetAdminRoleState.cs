using TelegramBotNM.Bot;
using TelegramBotNM.Repository.Commands.Contract;
using TelegramBotNM.StateMachineNM.State.MessageState;
using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.Bot.StateMachineNM.States;

public class SetAdminRoleState : MessageState
{
    private readonly IRecordUpdate<TelegramUser> _userUpdate;
    private readonly TelegramUser _user;

    public SetAdminRoleState(TelegramUser user, TelegramBotBridge bridge, IRecordUpdate<TelegramUser> userUpdate) : base(bridge, user.Id, "Congrats, you are admin now")
    {
        _userUpdate = userUpdate;
        _user = user;
    }

    protected override async Task OnEnter(CancellationToken token)
    {
        _userUpdate.Execute(_user with { Role = Role.Admin });
        
        await base.OnEnter(token);
    }
}