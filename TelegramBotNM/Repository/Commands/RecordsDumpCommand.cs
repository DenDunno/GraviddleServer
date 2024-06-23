using System.Data;
using TelegramBotNM.Parser;
using TelegramBotNM.Repository.Commands.Contract;
using IQueryProvider = TelegramBotNM.Repository.Query.IQueryProvider;

namespace TelegramBotNM.Repository.Commands;

public class RecordsDumpCommand<TRecord> : IRecordsDump<TRecord> 
{
    private readonly IRecordParser<TRecord> _parser;
    private readonly IQueryProvider _queryProvider;
    private readonly IDatabaseBridge _bridge;

    public RecordsDumpCommand(IDatabaseBridge bridge, IRecordParser<TRecord> parser, IQueryProvider queryProvider)
    {
        _queryProvider = queryProvider;
        _bridge = bridge;
        _parser = parser;
    }

    public IList<TRecord> Execute()
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