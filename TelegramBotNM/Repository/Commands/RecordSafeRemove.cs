using TelegramBotNM.Repository.Commands.Contract;

namespace TelegramBotNM.Repository.Commands;

public class RecordSafeRemove<TKey> : IRecordRemove<TKey> 
{
    private readonly IRecordContains<TKey> _containsCommand;
    private readonly IRecordRemove<TKey> _recordRemove;

    public RecordSafeRemove(IRecordContains<TKey> containsCommand, IRecordRemove<TKey> recordRemove)
    {
        _containsCommand = containsCommand;
        _recordRemove = recordRemove;
    }

    public void Execute(TKey key)
    {
        if (_containsCommand.Execute(key))
        {
            _recordRemove.Execute(key);
        }
    }
}