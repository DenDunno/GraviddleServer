namespace Domain.Factory;

public interface IFactory<out T>
{
    T Create();
}