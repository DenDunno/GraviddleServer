using TelegramBotNM.Bot;
using TelegramBotNM.Repository.Commands.Contract;
using TelegramBotNM.StateMachineNM.State.MessageState;

namespace GraviddleServer.Code.Bot.StateMachineNM.States;

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
        await base.OnEnter(token);
        _userRecordRemove.Execute(ChatId);
    }
}