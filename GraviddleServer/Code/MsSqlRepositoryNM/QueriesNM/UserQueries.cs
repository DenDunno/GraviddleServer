using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.MsSqlRepositoryNM.QueriesNM;

public class UserQueries 
{
    public string Insert(TelegramUser element)
    {
        return $@"INSERT INTO Users (ChatId, Role, ConversationState) VALUES 
                ({element.Id}, 
                {(int)element.Role},
                {element.ConversationState});";
    }

    public string Remove(long chatId)
    {
        return $@"DELETE FROM Users WHERE ChatId={chatId};";
    }

    public string UpdateConversation(TelegramUser element)
    {
        return $@"UPDATE Users SET ConversationState={element.ConversationState} WHERE ChatId={element.Id};";
    }
    
    public string UpdateRole(TelegramUser element)
    {
        return $@"UPDATE Users SET Role={(int)element.Role} WHERE ChatId={element.Id};";
    }

    public string Contains(long chatId)
    {
        return $@"SELECT COUNT(*) FROM Users WHERE ChatId={chatId};";
    }

    public string GetAll()
    {
        return $@"SELECT * FROM Users;";
    }

    public string Fetch(long chatId)
    {
        return $@"SELECT * FROM Users WHERE ChatId={chatId};";
    }
}