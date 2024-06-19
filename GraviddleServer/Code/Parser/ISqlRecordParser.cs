using System.Data.SqlClient;

namespace GraviddleServer.Code.Parser;

public interface ISqlRecordParser<out T>
{
    T Parse(SqlDataReader sqlDataReader);
}