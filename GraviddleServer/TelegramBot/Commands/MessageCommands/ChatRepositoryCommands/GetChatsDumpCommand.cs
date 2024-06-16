using System.Text;
using GraviddleServer.ChatRepository;
using GraviddleServer.TelegramBot.Commands;
using GraviddleServer.Utils;

namespace GraviddleServer.TelegramBot;

public class GetChatsDumpCommand : IMessageCommand
{
    private readonly IChatsRepository _chatsRepository;
    private readonly TelegramBotBridge _bridge;
    
    public GetChatsDumpCommand(IChatsRepository chatsRepository, TelegramBotBridge bridge)
    {
        _chatsRepository = chatsRepository;
        _bridge = bridge;
    }
    
    public async Task Handle(long chatId, CancellationToken token)
    {
        IEnumerable<long> chatsId = _chatsRepository.GetAllChats();
        string chatsRecord = GetChatsRecord(chatsId);
        await _bridge.SendMessage(chatsRecord, chatId, token);
    }

    private static string GetChatsRecord(IEnumerable<long> chatsId)
    {
        int index = 1;
        StringBuilder stringBuilder = new();
        stringBuilder.Append("Chats id:");
        chatsId.ForEach(chatId => stringBuilder.Append($"\n{index++}. {chatId}"));
        
        return stringBuilder.ToString();
    }
}