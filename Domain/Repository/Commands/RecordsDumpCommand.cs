using System.Data;
using Domain.Parser;
using Domain.Repository.Commands.Contract;
using Query_IQueryProvider = Domain.Repository.Query.IQueryProvider;

namespace Domain.Repository.Commands;

public class RecordsDumpCommand<TRecord> : IRecordsDump<TRecord> 
{
    private readonly IRecordParser<TRecord> _parser;
    private readonly Query_IQueryProvider _queryProvider;
    private readonly IDatabaseBridge _bridge;

    public RecordsDumpCommand(IDatabaseBridge bridge, IRecordParser<TRecord> parser, Query_IQueryProvider queryProvider)
    {
        _queryProvider = queryProvider;
        _bridge = bridge;
        _parser = parser;
    }

    public List<TRecord> Execute()
    {
        List<TRecord> elements = new();
        using IDataReader reader = _bridge.ExecuteReader(_queryProvider.Value);
        
        while (reader.Read())
        {
            elements.Add(_parser.Parse(reader));
        }

        reader.Close();
        
        return elements;
    }
}