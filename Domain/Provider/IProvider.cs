namespace Domain.Provider;

public interface IProvider<T>
{
    Task<T> Provide();
}