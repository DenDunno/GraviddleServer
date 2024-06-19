namespace GraviddleServer.Code.Repository;

public class SecureRepository<T> : IRepository<T>
{
    private readonly IRepository<T> _repository;

    public SecureRepository(IRepository<T> repository)
    {
        _repository = repository;
    }

    public void Add(T element)
    {
        if (_repository.Contains(element) == false)
        {
            _repository.Add(element);
        }
    }

    public void Remove(T element)
    {
        if (_repository.Contains(element))
        {
            _repository.Remove(element);
        }
    }

    public bool Contains(T element)
    {
        return _repository.Contains(element);
    }

    public IEnumerable<T> GetAll()
    {
        return _repository.GetAll();
    }
}