using System.Text;
using GraviddleServer.ChatRepository;
using GraviddleServer.TelegramBot.Commands;
using GraviddleServer.Utils;
using Telegram.Bot.Types;

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
        string chatsRecord = await GetChatsRecord(chatsId);
        await _bridge.SendMessage(chatsRecord, chatId, token);
    }

    private async Task<string> GetChatsRecord(IEnumerable<long> chatsId)
    {
        int index = 1;
        StringBuilder builder = new();
        Chat[] chats = await _bridge.GetChats(chatsId);
        
        builder.Append("Chats:");
        chats.ForEach(chat => builder.Append($"\n{index++}. Name = {chat.GetFullName()} ID = {chat.Id}"));
        
        return builder.ToString();
    }
}