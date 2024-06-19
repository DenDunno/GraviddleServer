namespace GraviddleServer.Code.Repository;

public class SessionRepository<T> : IRepository<T>
{
    private readonly List<T> _sessionList = new();

    public IEnumerable<T> GetAll()
    {
        return _sessionList;
    }

    public void Add(T element)
    {
        _sessionList.Add(element);
    }

    public void Remove(T element)
    {
        _sessionList.Remove(element);
    }

    public bool Contains(T element)
    {
        return _sessionList.Contains(element);
    }
}