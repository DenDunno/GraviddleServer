namespace GraviddleServer.Code.Repository;

public interface IRepository<T> : IDump<T>
{
    void Add(T element);
    void Remove(T element);
    bool Contains(T element);
}