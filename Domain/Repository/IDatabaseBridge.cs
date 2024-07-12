using System.Data;

namespace Domain.Repository;

public interface IDatabaseBridge : IDisposable
{
    void Open();
    int ExecuteNonQuery(string query);
    int ExecuteScalar(string query);
    IDataReader ExecuteReader(string query);
}