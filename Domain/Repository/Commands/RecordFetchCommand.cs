using System.Data;
using Domain.Parser;
using Domain.Repository.Commands.Contract;
using Domain.Repository.Query;

namespace Domain.Repository.Commands;

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
        IDataReader reader = null!;
        bool result;
        
        try
        {
            reader = Bridge.ExecuteReader(GetQuery(key));
            record = reader.Read() ? _parser.Parse(reader) : default!;
            result = true;
        }
        finally
        {
            reader.Close();
        }
        
        
        return result;
    }
}