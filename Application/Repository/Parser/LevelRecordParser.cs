using System.Data;
using Application.Records;
using Domain.Parser;

namespace Application.Repository.Parser;

public class LevelRecordParser : IRecordParser<LevelRecord>
{
    public LevelRecord Parse(IDataReader dataReader)
    {
        return new LevelRecord(
            Id:(string)dataReader[0],
            Name: (string)dataReader[1],
            Stars: (int)dataReader[2],
            Level: (string)dataReader[3],
            LevelIndex: (int)dataReader[4],
            Time: (double)dataReader[5],
            DeathCount: (int)dataReader[6]
        );
    }
}