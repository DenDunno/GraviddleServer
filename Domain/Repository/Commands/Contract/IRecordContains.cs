namespace Domain.Repository.Commands.Contract;

public interface IRecordContains<in TKey>
{
    Task<bool> Execute(TKey key);
}