using TelegramBotTemplate.User;

namespace TelegramBotTemplate.Repository;

public class TelegramUserQueries 
{
    public string Insert(TelegramUser element)
    {
        return $@"INSERT INTO TelegramUser (ChatId, Role, ConversationState) VALUES 
                ({element.Id}, 
                {(int)element.Role},
                {element.ConversationState});";
    }

    public string Remove(long chatId)
    {
        return $@"DELETE FROM TelegramUser WHERE ChatId={chatId};";
    }

    public string UpdateConversation(TelegramUser element)
    {
        return $@"UPDATE TelegramUser SET ConversationState={element.ConversationState} WHERE ChatId={element.Id};";
    }
    
    public string UpdateRole(TelegramUser element)
    {
        return $@"UPDATE TelegramUser SET Role={(int)element.Role} WHERE ChatId={element.Id};";
    }

    public string Contains(long chatId)
    {
        return $@"SELECT COUNT(*) FROM TelegramUser WHERE ChatId={chatId};";
    }

    public string GetAll()
    {
        return $@"SELECT * FROM TelegramUser;";
    }

    public string Fetch(long chatId)
    {
        return $@"SELECT * FROM TelegramUser WHERE ChatId={chatId};";
    }
}