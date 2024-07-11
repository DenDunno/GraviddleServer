namespace GraviddleServer.Code.Bot;

public class ConstantProvider<T> : IProvider<T>
{
    private readonly T _constant;

    public ConstantProvider(T constant)
    {
        _constant = constant;
    }

    public T Provide()
    {
        return _constant;
    }
}