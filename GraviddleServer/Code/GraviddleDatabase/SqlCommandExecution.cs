using System.Data.SqlClient;

namespace GraviddleServer.Code.GraviddleDatabase;

public class SqlCommandExecution : IDisposable
{
    private readonly SqlConnection _sqlConnection;

    public SqlCommandExecution(string connectionString)
    {
        _sqlConnection = new SqlConnection(connectionString);
    }

    public void Open()
    {
        _sqlConnection.Open();
    }

    public int Execute(string query)
    {
        return new SqlCommand(query, _sqlConnection).ExecuteNonQuery();
    }

    public void Dispose()
    {
        _sqlConnection.Dispose();
    }
}