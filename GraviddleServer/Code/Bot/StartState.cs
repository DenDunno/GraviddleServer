using Telegram.Bot.Types;
using TelegramBotNM.Repository.Commands.Contract;
using TelegramBotNM.StateMachineNM;
using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.Bot;

public class StartState : BaseState
{
    private readonly IRecordAdd<TelegramUser> _recordAdd;

    public StartState(IRecordAdd<TelegramUser> recordAdd)
    {
        _recordAdd = recordAdd;
    }

    protected override Task OnEnter(Message message, CancellationToken token)
    {
        //_recordAdd.Execute();
        
        return Task.CompletedTask;
    }
}