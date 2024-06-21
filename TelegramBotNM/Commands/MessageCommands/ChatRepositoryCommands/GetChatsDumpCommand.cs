using System.Text;
using Telegram.Bot.Types;
using TelegramBotNM.Bot;
using TelegramBotNM.Extensions;
using TelegramBotNM.Repository;
using TelegramBotNM.Repository.Commands.Contract;

namespace TelegramBotNM.Commands.MessageCommands.ChatRepositoryCommands;

public class GetChatsDumpCommand : IMessageCommand
{
    private readonly IRecordsDump<long> _chatsRecordsDump;
    private readonly TelegramBotBridge _bridge;
    
    public GetChatsDumpCommand(IRecordsDump<long> chatsRecordsDump, TelegramBotBridge bridge)
    {
        _chatsRecordsDump = chatsRecordsDump;
        _bridge = bridge;
    }
    
    public async Task Handle(Message message, CancellationToken token)
    {
        IEnumerable<long> chatsId = _chatsRecordsDump.Execute();
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