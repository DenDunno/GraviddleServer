using TelegramBotNM.Repository.Commands.Contract;
using TelegramBotNM.StateMachineNM.State;
using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.Bot.StateMachineNM.States;

public class StartState : BaseState
{
    private readonly IRecordAdd<TelegramUser, long> _recordAdd;
    private readonly TelegramUser _user;

    public StartState(IRecordAdd<TelegramUser, long> recordAdd, TelegramUser user) : base("Start")
    {
        _recordAdd = recordAdd;
        _user = user;
    }

    protected override Task OnEnter(CancellationToken token)
    {
        _recordAdd.Execute(_user);
        
        return Task.CompletedTask;
    }
}