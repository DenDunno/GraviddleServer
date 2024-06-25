using System.Data;
using TelegramBotNM.Parser;

namespace GraviddleServer.Code.Parser;

public class LevelRecordParser : IRecordParser<LevelRecord>
{
    public LevelRecord Parse(IDataReader dataReader)
    {
        return new LevelRecord(
            Id:(string)dataReader[0],
            Name: (string)dataReader[1],
            Stars: (int)dataReader[2],
            Level: (string)dataReader[3],
            Time: (double)dataReader[4],
            DeathCount: (int)dataReader[5]
        );
    }
}