using System.Data;
using AnalyticsTelegramBot.Provider;
using Application.Repository;
using Domain.Parser;
using Domain.Repository.Commands.Contract;

namespace Domain.Repository.Commands;

public class RecordsDumpCommand<TRecord> : IRecordsDump<TRecord>
{
    private readonly IProvider<string> _queryProvider;
    private readonly IRecordParser<TRecord> _parser;
    private readonly DatabaseConnection _connection;

    public RecordsDumpCommand(DatabaseConnection connection, IRecordParser<TRecord> parser, IProvider<string> queryProvider)
    {
        _queryProvider = queryProvider;
        _connection = connection;
        _parser = parser;
    }

    public async Task<List<TRecord>> Execute()
    {
        List<TRecord> elements = new();
        string query = await _queryProvider.Provide();
        
        using IDatabaseBridge bridge = _connection.Open();
        using IDataReader reader = await bridge.ExecuteReader(query);

        while (reader.Read())
        {
            elements.Add(_parser.Parse(reader));
        }

        reader.Close();

        return elements;
    }
}