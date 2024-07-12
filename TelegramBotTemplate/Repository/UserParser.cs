using System.Data;
using Domain.Parser;
using TelegramBotTemplate.User;

namespace TelegramBotTemplate.Repository;

public class UserParser : IRecordParser<TelegramUser>
{
    public TelegramUser Parse(IDataReader dataReader)
    {
        return new TelegramUser((long)dataReader[0], (Role)dataReader[1], (int)dataReader[2]);
    }
}