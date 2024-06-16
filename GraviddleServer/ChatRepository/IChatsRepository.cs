namespace GraviddleServer.ChatRepository;

public interface IChatsRepository : IChatsDump
{
    bool TryAdd(long chatId);
    bool TryRemove(long chatId);
}