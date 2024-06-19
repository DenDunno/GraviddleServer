using System.Text;
using GraviddleServer.ChatRepository;
using GraviddleServer.Code.Utils;
using Telegram.Bot.Types;

namespace GraviddleServer.Code.TelegramBot.Commands.MessageCommands.ChatRepositoryCommands;

public class GetChatsDumpCommand : IMessageCommand
{
    private readonly IChatsRepository _repository;
    private readonly TelegramBotBridge _bridge;
    
    public GetChatsDumpCommand(IChatsRepository repository, TelegramBotBridge bridge)
    {
        _repository = repository;
        _bridge = bridge;
    }
    
    public async Task Handle(long chatId, CancellationToken token)
    {
        IEnumerable<long> chatsId = _repository.GetAllChats();
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