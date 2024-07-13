using System.Data;

namespace Domain.Repository;

public interface IDatabaseBridge : IDisposable
{
    void Open();
    Task<int> ExecuteNonQuery(string query);
    Task<int> ExecuteScalar(string query);
    Task<IDataReader> ExecuteReader(string query);
}