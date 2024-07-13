using System.Data;
using System.Data.SqlClient;
using Domain.Repository;

namespace Application.Repository;

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

    public async Task<int> ExecuteNonQuery(string query)
    {
        return await CreateCommand(query).ExecuteNonQueryAsync();
    }

    public async Task<int> ExecuteScalar(string query)
    {
        return (int)(await CreateCommand(query).ExecuteScalarAsync())!;
    }

    public async Task<IDataReader> ExecuteReader(string query)
    {
        return await CreateCommand(query).ExecuteReaderAsync();
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