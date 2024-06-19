using GraviddleServer.ChatRepository;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace GraviddleServer.Code.TelegramBot;

public class TelegramBotBridge
{
    private readonly ITelegramBotClient _client;
    private readonly IChatsDump _dump;

    public TelegramBotBridge(ITelegramBotClient client, IChatsDump dump)
    {
        _dump = dump;
        _client = client;
    }
    
    public async Task SendMessage(string text, long chatId, CancellationToken token)
    {
        await _client.SendTextMessageAsync(chatId, text, cancellationToken: token);
    }

    public async Task SendToAll(string text)
    {
        IEnumerable<long> chats = _dump.GetAllChats();
        IEnumerable<Task<Message>> tasks = chats.Select(chatId => _client.SendTextMessageAsync(chatId, text));

        await Task.WhenAll(tasks);
    }

    public async Task<Chat[]> GetChats(IEnumerable<long> chatIds)
    {
        IEnumerable<Task<Chat>> tasks = chatIds.Select(id => _client.GetChatAsync(id));
        return await Task.WhenAll(tasks);
    }
}