namespace Domain.Repository;

public interface IDatabaseModel<out TKey>
{
    TKey Id { get; }
}