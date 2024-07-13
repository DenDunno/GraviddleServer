namespace Domain.Repository.Commands.Contract;

public interface IRecordRemove<in TKey> 
{
    Task Execute(TKey key);
}