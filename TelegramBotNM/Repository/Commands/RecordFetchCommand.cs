using System.Data;
using TelegramBotNM.Parser;
using TelegramBotNM.Repository.Commands.Contract;
using TelegramBotNM.Repository.Query;

namespace TelegramBotNM.Repository.Commands;

public class RecordFetchCommand<TRecord, TKey> : RecordBaseCommand<TKey>, IRecordFetch<TRecord, TKey>
{
    private readonly IRecordParser<TRecord> _parser;
    
    public RecordFetchCommand(IDatabaseBridge bridge, IRecordParser<TRecord> parser, IQueryBuilder<TKey> queryBuilder) 
        : base(bridge, queryBuilder)
    {
        _parser = parser;
    }

    public TRecord Execute(TKey key)
    {
        IDataReader reader = Bridge.ExecuteReader(GetQuery(key));
        
        if (reader.Read() == false)
        {
            throw new InvalidOperationException("No records were returned.");
        }

        TRecord result = _parser.Parse(reader);

        if (reader.Read())
        {
            throw new InvalidOperationException("Multiple records were returned.");
        }

        return result;
    }
}