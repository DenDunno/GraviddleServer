using Domain.Repository.Commands.Contract;
using TelegramBotTemplate.Bot;
using TelegramBotTemplate.StateMachineNM.State.MessageState;
using TelegramBotTemplate.User;

namespace AnalyticsTelegramBot.StateMachineNM.StatesNM;

public class SetAdminRoleState : MessageState
{
    private readonly IRecordUpdate<TelegramUser> _userUpdate;
    private readonly TelegramUser _user;

    public SetAdminRoleState(TelegramUser user, TelegramBotBridge bridge, IRecordUpdate<TelegramUser> userUpdate) :
        base(bridge, user.Id, "Congrats, you are admin now")
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