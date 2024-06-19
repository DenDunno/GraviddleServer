using GraviddleServer.Code.Repository;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace GraviddleServer.Code.TelegramBotNM;

public class TelegramBotBridge
{
    private readonly ITelegramBotClient _client;
    private readonly IDump<long> _chatsDump;

    public TelegramBotBridge(ITelegramBotClient client, IDump<long> chatsDump)
    {
        _chatsDump = chatsDump;
        _client = client;
    }
    
    public async Task SendMessage(string text, long chatId, CancellationToken token)
    {
        await _client.SendTextMessageAsync(chatId, text, cancellationToken: token);
    }

    public async Task SendToAll(string text)
    {
        IEnumerable<long> chats = _chatsDump.GetAll();
        IEnumerable<Task<Message>> tasks = chats.Select(chatId => _client.SendTextMessageAsync(chatId, text));

        await Task.WhenAll(tasks);
    }

    public async Task<Chat[]> GetChats(IEnumerable<long> chatIds)
    {
        IEnumerable<Task<Chat>> tasks = chatIds.Select(id => _client.GetChatAsync(id));
        return await Task.WhenAll(tasks);
    }
}