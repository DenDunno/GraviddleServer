using System.Data;

namespace Domain.Parser;

public interface IRecordParser<out T>
{
    T Parse(IDataReader dataReader);
}