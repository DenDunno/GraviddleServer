namespace Domain.Repository.Commands.Contract;

public interface IRecordAdd<in TRecord, TKey> where TRecord : IDatabaseModel<TKey>
{
    Task Execute(TRecord element);
}