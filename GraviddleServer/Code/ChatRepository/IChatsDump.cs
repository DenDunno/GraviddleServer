namespace GraviddleServer.ChatRepository;

public interface IChatsDump
{
    IEnumerable<long> GetAllChats();
}