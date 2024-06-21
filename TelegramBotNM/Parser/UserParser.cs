using System.Data;
using TelegramBotNM.UserNM;

namespace TelegramBotNM.Parser;

public class UserParser : IRecordParser<TelegramUser>
{
    public TelegramUser Parse(IDataReader dataReader)
    {
        return new TelegramUser((long)dataReader[0], (Role)dataReader[1], (int)dataReader[2]);
    }
}