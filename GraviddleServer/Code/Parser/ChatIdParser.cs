using System.Data.SqlClient;

namespace GraviddleServer.Code.Parser;

public class ChatIdParser : ISqlRecordParser<long>
{
    public long Parse(SqlDataReader sqlDataReader)
    {
        return (long)sqlDataReader[0];
    }
}