using System.Data.SqlClient;

namespace GraviddleServer.Code.Parser;

public class LevelRecordParser : ISqlRecordParser<LevelRecord>
{
    public LevelRecord Parse(SqlDataReader sqlDataReader)
    {
        return new LevelRecord()
        {
            DeviceId = (string)sqlDataReader[0],
            Name = (string)sqlDataReader[1],
            Stars = (int)sqlDataReader[2],
            Level = (string)sqlDataReader[3],
            Time = (double)sqlDataReader[4],
            DeathCount = (int)sqlDataReader[5],
        };
    }
}