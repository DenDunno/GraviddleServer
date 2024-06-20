using System.Text;
using GraviddleServer.Code.Repository;
using GraviddleServer.Code.Utils;
using Telegram.Bot.Types;

namespace GraviddleServer.Code.TelegramBotNM.Commands.MessageCommands.ChatRepositoryCommands;

public class GetChatsDumpCommand : IMessageCommand
{
    private readonly IDump<long> _chatsDump;
    private readonly TelegramBotBridge _bridge;
    
    public GetChatsDumpCommand(IDump<long> chatsDump, TelegramBotBridge bridge)
    {
        _chatsDump = chatsDump;
        _bridge = bridge;
    }
    
    public async Task Handle(Message message, CancellationToken token)
    {
        IEnumerable<long> chatsId = _chatsDump.GetAll();
        string chatsRecord = await GetChatsRecord(chatsId);
        await _bridge.SendMessage(chatsRecord, message.Chat.Id, token);
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