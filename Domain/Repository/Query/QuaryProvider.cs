namespace Domain.Repository.Query;

public class QueryProvider : IQueryProvider
{
    public QueryProvider(string value)
    {
        Value = value;
    }

    public string Value { get; }
}