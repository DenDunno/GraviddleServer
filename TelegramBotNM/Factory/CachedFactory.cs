namespace TelegramBotNM.Factory;

public class CachedFactory<T> : IFactory<T>
{
    private readonly IFactory<T> _factory;
    private T? _cachedValue;

    public CachedFactory(IFactory<T> factory)
    {
        _factory = factory;
    }

    public T Create()
    {
        if (_cachedValue == null)
        {
            _cachedValue = _factory.Create();
        }

        return _cachedValue;
    }
}