using Application.Repository;
using Domain.Repository.Query;

namespace Domain.Repository.Commands;

public abstract class RecordBaseCommand<T>
{
    protected readonly DatabaseConnection Connection;
    private readonly IQueryBuilder<T> _queryBuilder;
    
    protected RecordBaseCommand(DatabaseConnection connection, IQueryBuilder<T> queryBuilder)
    {
        _queryBuilder = queryBuilder;
        Connection = connection;
    }

    protected string GetQuery(T input) => _queryBuilder.Build(input);
}