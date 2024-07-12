namespace Domain.Repository.Commands.Contract;

public interface IRecordUpdate<in T>
{
    void Execute(T element);
}