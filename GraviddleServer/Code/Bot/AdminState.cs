using TelegramBotNM.Bot;
using TelegramBotNM.Repository.Commands.Contract;
using TelegramBotNM.StateMachineNM;
using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.Bot;

public class AdminState : MessageState
{
    private readonly IRecordUpdate<TelegramUser> _userUpdate;
    private readonly TelegramUser _user;

    public AdminState(TelegramUser user, TelegramBotBridge bridge, IRecordUpdate<TelegramUser> userUpdate) : base(
        bridge, "Congrats, you are admin now", user.Id)
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