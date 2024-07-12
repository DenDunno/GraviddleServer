using Domain.Repository.Commands.Contract;

namespace TelegramBotTemplate.Router.Commands;

public class StopCommand : IBotCommand<long>
{
    private readonly IRecordRemove<long> _userRecordRemove;

    public StopCommand(IRecordRemove<long> userRecordRemove)
    {
        _userRecordRemove = userRecordRemove;
    }

    public Task Handle(long input, CancellationToken token)
    {
        _userRecordRemove.Execute(input);
        return Task.CompletedTask;
    }
}