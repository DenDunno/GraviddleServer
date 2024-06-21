using System.Data;

namespace TelegramBotNM.Parser;

public interface IRecordParser<out T>
{
    T Parse(IDataReader dataReader);
}