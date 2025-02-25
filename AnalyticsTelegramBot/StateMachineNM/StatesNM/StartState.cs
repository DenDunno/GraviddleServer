using Domain.Repository.Commands.Contract;
using TelegramBotTemplate.Bot;
using TelegramBotTemplate.StateMachineNM.State.MessageState;
using TelegramBotTemplate.User;

namespace AnalyticsTelegramBot.StateMachineNM.StatesNM;

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
        await _recordAdd.Execute(_user);
        await base.OnEnter(token);
    }
}