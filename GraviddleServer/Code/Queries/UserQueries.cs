using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.Queries;

public class UserQueries 
{
    public string Insert(TelegramUser element)
    {
        return $@"INSERT INTO ChatId VALUES ({element.ChatId});";
    }

    public string Remove(TelegramUser element)
    {
        return $@"DELETE FROM ChatId WHERE ChatId={element.ChatId};";
    }
    
    public string Update(TelegramUser element)
    {
        return $@"UPDATE ChatId SET ConversationId={element.ConversationState} WHERE ChatId={element.ChatId};";
    }

    public string Contains(long chatId)
    {
        return $@"SELECT COUNT(*) FROM ChatId WHERE ChatId={chatId};";
    }

    public string GetAll()
    {
        return $@"SELECT * FROM ChatId;";
    }

    public string Fetch(long chatId)
    {
        return $@"SELECT * FROM ChatId WHERE ChatId={chatId};";
    }
}