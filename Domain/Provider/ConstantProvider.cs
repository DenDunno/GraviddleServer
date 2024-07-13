namespace Domain.Provider;

public class ConstantProvider<T> : IProvider<T>
{
    private readonly T _constant;

    public ConstantProvider(T constant)
    {
        _constant = constant;
    }

    public Task<T> Provide()
    {
        return Task.FromResult(_constant);
    }
}