using Domain.Repository.Commands.Contract;
using TelegramBotTemplate.Bot;
using TelegramBotTemplate.StateMachineNM.State.MessageState;
using TelegramBotTemplate.User;

namespace AnalyticsTelegramBot.StateMachineNM.StatesNM;

public class SetAdminRoleState : MessageState
{
    private readonly IRecordUpdate<TelegramUser> _userUpdate;
    private readonly TelegramBotBridge _bridge;
    private readonly TelegramUser _user;
    private readonly int _messageId;

    public SetAdminRoleState(int messageId, TelegramUser user, TelegramBotBridge bridge,
        IRecordUpdate<TelegramUser> userUpdate) :
        base(bridge, user.Id, "<b><i>*Password deleted*</i></b> Congrats, you are admin now")
    {
        _userUpdate = userUpdate;
        _messageId = messageId;
        _bridge = bridge;
        _user = user;
    }

    protected override async Task OnEnter(CancellationToken token)
    {
        await _bridge.Delete(_user.Id, _messageId, token);
        _userUpdate.Execute(_user with { Role = Role.Admin });

        await base.OnEnter(token);
    }
}