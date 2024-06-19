namespace GraviddleServer.Code.Queries;

public class ChatQueries : IQueries<long>
{
    public string Insert(long element)
    {
        return $@"INSERT INTO ChatId VALUES ({element});";
    }

    public string Remove(long element)
    {
        return $@"DELETE FROM ChatId WHERE ChatId={element};";
    }

    public string Contains(long element)
    {
        return $@"SELECT COUNT(*) FROM ChatId WHERE ChatId={element};";
    }

    public string GetAll()
    {
        return $@"SELECT * FROM ChatId;";
    }
}