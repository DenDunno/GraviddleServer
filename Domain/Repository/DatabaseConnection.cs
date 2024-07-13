using Domain.Factory;

namespace Domain.Repository;

public class DatabaseConnection
{
    private readonly IFactory<IDatabaseBridge> _databaseBridgeFactory;

    public DatabaseConnection(IFactory<IDatabaseBridge> databaseBridgeFactory)
    {
        _databaseBridgeFactory = databaseBridgeFactory;
    }

    public IDatabaseBridge Open()
    {
        IDatabaseBridge bridge = _databaseBridgeFactory.Create();
        bridge.Open();

        return bridge;
    }
}