namespace Domain.Repository.Commands.Contract;

public interface IRecordUpdate<in T>
{
    Task Execute(T element);
}