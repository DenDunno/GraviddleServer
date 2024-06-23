using TelegramBotNM.Repository.Query;
using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.MsSqlRepositoryNM.QueriesNM;

public class UserQueries : Queries<TelegramUser, long>
{
    public override string Insert(TelegramUser element)
    {
        return $@"INSERT INTO Users (ChatId, Role, ConversationState) VALUES 
                ({element.ChatId}, 
                {(int)element.Role},
                {element.ConversationState});";
    }

    public override string Remove(long chatId)
    {
        return $@"DELETE FROM Users WHERE ChatId={chatId};";
    }

    public override string Update(TelegramUser element)
    {
        return $@"UPDATE Users SET ConversationId={element.ConversationState} WHERE ChatId={element.ChatId};";
    }

    public override string Contains(long chatId)
    {
        return $@"SELECT COUNT(*) FROM Users WHERE ChatId={chatId};";
    }

    public override string GetAll()
    {
        return $@"SELECT * FROM Users;";
    }

    public override string Fetch(long chatId)
    {
        return $@"SELECT * FROM Users WHERE ChatId={chatId};";
    }
}