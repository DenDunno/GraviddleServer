using System.Data;
using Domain.Parser;
using Domain.Repository.Commands.Contract;
using Domain.Repository.Query;

namespace Domain.Repository.Commands.Fetch;

public class RecordFetchCommand<TRecord, TKey> : RecordBaseCommand<TKey>, IRecordFetch<TRecord, TKey> 
{
    private readonly IRecordParser<TRecord> _parser;
    
    public RecordFetchCommand(DatabaseConnection connection, IRecordParser<TRecord> parser, IQueryBuilder<TKey> queryBuilder) 
        : base(connection, queryBuilder)
    {
        _parser = parser;
    }

    public async Task<FetchResult<TRecord>> Execute(TKey key)
    {
        using IDatabaseBridge bridge = Connection.Open();
        IDataReader reader = await bridge.ExecuteReader(GetQuery(key));
        TRecord record = reader.Read() ? _parser.Parse(reader) : default!;
        reader.Close();
        
        return new FetchResult<TRecord>(record != null, record);
    }
}