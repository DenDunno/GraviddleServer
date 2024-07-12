namespace Domain.Repository.Commands.Contract;

public interface IRecordContains<in TKey>
{
    bool Execute(TKey key);
}