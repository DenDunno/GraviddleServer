using System.Data;
using System.Data.SqlClient;
using TelegramBotNM.Repository;

namespace GraviddleServer.Code.MsSqlRepositoryNM;

public class MsSqlDatabaseBridge : IDatabaseBridge
{
    private readonly SqlConnection _sqlConnection;

    public MsSqlDatabaseBridge(string connectionString)
    {
        _sqlConnection = new SqlConnection(connectionString);
    }

    public void Open()
    {
        _sqlConnection.Open();
    }

    public int ExecuteNonQuery(string query)
    {
        return CreateCommand(query).ExecuteNonQuery();
    }

    public int ExecuteScalar(string query)
    {
        return (int)CreateCommand(query).ExecuteScalar();
    }

    public IDataReader ExecuteReader(string query)
    {
        return CreateCommand(query).ExecuteReader();
    }

    private SqlCommand CreateCommand(string query)
    {
        return new SqlCommand(query, _sqlConnection);
    }

    public void Dispose()
    {
        _sqlConnection.Dispose();
    }
}