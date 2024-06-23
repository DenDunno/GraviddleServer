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

    public bool TryExecute(TKey key, out TRecord record)
    {
        IDataReader reader = Bridge.ExecuteReader(GetQuery(key));
        record = reader.Read() ? _parser.Parse(reader) : default!;
        reader.Close();
        
        return record != null;
    }
}