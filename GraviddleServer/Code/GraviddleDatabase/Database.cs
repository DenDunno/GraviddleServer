using GraviddleServer.Code.Level;

namespace GraviddleServer.Code.GraviddleDatabase;

public class Database : IDisposable
{
    private readonly DatabaseQueries _databaseQueries = new();
    private readonly SqlCommandExecution _sqlCommandExecution;

    public Database(string connectionString)
    {
        _sqlCommandExecution = new SqlCommandExecution(connectionString);
    }
    
    public void Open()
    {
        _sqlCommandExecution.Open();
    }

    public void InsertRecord(LevelResult levelResult)
    {
        _sqlCommandExecution.Execute(_databaseQueries.InsertCommand(levelResult));
    }

    public void Dispose()
    {
        _sqlCommandExecution.Dispose();
    }
}