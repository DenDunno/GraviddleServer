namespace Domain.Repository.Query;

public class QueryBuilder<T> : IQueryBuilder<T>
{
    private readonly Func<T, string> _builder;

    public QueryBuilder(Func<T, string> builder)
    {
        _builder = builder;
    }

    public string Build(T element)
    {
        return _builder(element);
    }
}