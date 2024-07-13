using Domain.Repository.Commands.Contract;

namespace Domain.Repository.Commands;

public class RecordSafeAdd<TRecord, TKey> : IRecordAdd<TRecord, TKey> where TRecord : IDatabaseModel<TKey>
{
    private readonly IRecordContains<TKey> _containsCommand;
    private readonly IRecordAdd<TRecord, TKey> _addCommand;

    public RecordSafeAdd(IRecordContains<TKey> containsCommand, IRecordAdd<TRecord, TKey> addCommand)
    {
        _containsCommand = containsCommand;
        _addCommand = addCommand;
    }

    public async Task Execute(TRecord element)
    {
        if (await _containsCommand.Execute(element.Id) == false)
        {
            await _addCommand.Execute(element);
        }
    }
}