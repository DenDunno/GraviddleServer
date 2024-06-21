using System.Data;

namespace TelegramBotNM.Parser;

public class ElementParser<T> : IRecordParser<T>
{
    public T Parse(IDataReader dataReader)
    {
        return (T)dataReader[0];
    }
}