using TelegramBotNM.Bot;
using TelegramBotNM.Repository.Commands.Contract;
using TelegramBotNM.StateMachineNM.State.MessageState;
using TelegramBotNM.User;

namespace GraviddleServer.Code.Bot.StateMachineNM.StatesNM;

public class StartState : MessageState
{
    private readonly IRecordAdd<TelegramUser, long> _recordAdd;
    private readonly TelegramUser _user;

    public StartState(TelegramBotBridge bridge, TelegramUser user, IRecordAdd<TelegramUser, long> telegramUsersAdd) 
        : base(bridge, user.Id, "You will now receive analytics notifications. Enter the command 'stop' to terminate the bot.")
    {
        _recordAdd = telegramUsersAdd;
        _user = user;
    }

    protected override async Task OnEnter(CancellationToken token)
    {
        _recordAdd.Execute(_user);
        await base.OnEnter(token);
    }
}