using System.Data;

namespace TelegramBotNM.Repository;

public interface IDatabaseBridge : IDisposable
{
    void Open();
    int ExecuteNonQuery(string query);
    int ExecuteScalar(string query);
    IDataReader ExecuteReader(string query);
}