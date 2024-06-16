namespace GraviddleServer.ChatRepository;

public class MSSQLChatsRepository : IChatsRepository
{
    public bool TryAdd(long chatId)
    {
        return true;
    }

    public bool TryRemove(long chatId)
    {
        return true;
    }

    public IEnumerable<long> GetAllChats()
    {
        return ArraySegment<long>.Empty;
    }
}