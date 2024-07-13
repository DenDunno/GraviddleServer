namespace Domain.Repository.Commands.Contract;

public interface IRecordsDump<T>
{
    Task<List<T>> Execute();
}