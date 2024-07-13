using Domain.Repository.Commands.Contract;
using TelegramBotTemplate.Bot;
using TelegramBotTemplate.StateMachineNM.State.MessageState;

namespace AnalyticsTelegramBot.StateMachineNM.StatesNM;

public class StopState : MessageState
{
    private readonly IRecordRemove<long> _userRecordRemove;
    
    public StopState(TelegramBotBridge bridge, long chatId, IRecordRemove<long> userRecordRemove) 
        : base(bridge, chatId, "You will no longer receive any notifications")
    {
        _userRecordRemove = userRecordRemove;
    }

    protected override async Task OnEnter(CancellationToken token)
    {
        await _userRecordRemove.Execute(ChatId);
        await base.OnEnter(token);
    }
}