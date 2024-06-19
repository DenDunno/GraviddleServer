using System.Data.SqlClient;
using GraviddleServer.Code.Parser;
using GraviddleServer.Code.Queries;

namespace GraviddleServer.Code.Repository;

public class MsSqlRepository<T> : IRepository<T>
{
    private readonly MsSqlDatabaseBridge _msSqlDatabaseBridge;
    private readonly ISqlRecordParser<T> _parser;
    private readonly IQueries<T> _queries;

    public MsSqlRepository(MsSqlDatabaseBridge msSqlDatabaseBridge, IQueries<T> queries, ISqlRecordParser<T> parser)
    {
        _msSqlDatabaseBridge = msSqlDatabaseBridge;
        _queries = queries;
        _parser = parser;
    }

    public void Add(T element)
    {
        _msSqlDatabaseBridge.ExecuteNonQuery(_queries.Insert(element));
    }

    public void Remove(T element)
    {
        _msSqlDatabaseBridge.ExecuteNonQuery(_queries.Remove(element));
    }

    public bool Contains(T element)
    {
        return _msSqlDatabaseBridge.ExecuteScalar(_queries.Contains(element)) > 0;
    }

    public IEnumerable<T> GetAll()
    {
        List<T> elements = new();
        using SqlDataReader reader = _msSqlDatabaseBridge.ExecuteReader(_queries.GetAll());
        
        while (reader.Read())
        {
            elements.Add(_parser.Parse(reader));
        }

        return elements;
    }
}