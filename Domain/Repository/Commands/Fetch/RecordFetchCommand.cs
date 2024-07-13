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

    public async Task<FetchResult<TRecord>> Execute(TKey key)
    {
        IDataReader reader = await Bridge.ExecuteReader(GetQuery(key));
        TRecord record = reader.Read() ? _parser.Parse(reader) : default!;
        reader.Close();
        
        return new FetchResult<TRecord>(record != null, record);
    }
}