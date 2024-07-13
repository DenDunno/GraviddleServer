using Domain.Factory;
using Domain.Repository;

namespace Application.Repository;

public class MsSqlDatabaseBridgeFactory : IFactory<IDatabaseBridge>
{
    private readonly string _connectionString;

    public MsSqlDatabaseBridgeFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDatabaseBridge Create()
    {
        return new MsSqlDatabaseBridge(_connectionString);
    }
}