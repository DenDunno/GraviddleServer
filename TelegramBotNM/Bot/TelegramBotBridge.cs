using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotNM.Repository.Commands.Contract;
using TelegramBotNM.UserNM;

namespace TelegramBotNM.Bot;

public class TelegramBotBridge
{
    private readonly IRecordsDump<TelegramUser> _userRecordsDump;
    private readonly ITelegramBotClient _client;

    public TelegramBotBridge(ITelegramBotClient client, IRecordsDump<TelegramUser> userRecordsDump)
    {
        _userRecordsDump = userRecordsDump;
        _client = client;
    }
    
    public async Task SendMessage(string text, long chatId, CancellationToken token)
    {
        await _client.SendTextMessageAsync(chatId, text, cancellationToken: token);
    }

    public async Task SendToAll(string text)
    {
        IEnumerable<TelegramUser> users = _userRecordsDump.Execute();
        IEnumerable<Task<Message>> tasks = users.Select(user => _client.SendTextMessageAsync(user.Id, text));

        await Task.WhenAll(tasks);
    }

    public async Task<Chat[]> GetChats(IEnumerable<long> chatIds)
    {
        IEnumerable<Task<Chat>> tasks = chatIds.Select(id => _client.GetChatAsync(id));
        return await Task.WhenAll(tasks);
    }
}