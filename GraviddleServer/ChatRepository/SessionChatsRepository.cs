namespace GraviddleServer.ChatRepository;

public class SessionChatsRepository : IChatsRepository
{
    private readonly List<long> _chats = new();
    
    public bool TryAdd(long chatId)
    {
        bool chatDoesNotExist = _chats.Contains(chatId) == false;
        
        if (chatDoesNotExist)
        {
            _chats.Add(chatId);
        }
        
        return chatDoesNotExist;
    }

    public bool TryRemove(long chatId)
    {
        bool chatExists = _chats.Contains(chatId);
        
        if (chatExists)
        {
            _chats.Remove(chatId);
        }

        return chatExists;
    }

    public IEnumerable<long> GetAllChats()
    {
        return _chats;
    }
}