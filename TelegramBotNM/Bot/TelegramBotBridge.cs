using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotNM.Repository.Commands.Contract;
using TelegramBotNM.StateMachineNM.State.MessageState;
using TelegramBotNM.User;

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
    
    public async Task Send(ITelegramMessage message, long chatId, CancellationToken token = default)
    {
        await Send(await message.GetText(), chatId, message.Mode, token);
    }

    private async Task Send(string text, long chatId, ParseMode? mode = null, CancellationToken token = default)
    {
        await _client.SendTextMessageAsync(chatId, text, parseMode:mode, cancellationToken: token);
    }
    
    public async Task Send(string text, IEnumerable<long> chats, ParseMode? mode = null, CancellationToken token = default)
    {
        IEnumerable<Task> tasks = chats.Select(chatId => Send(text, chatId, mode, token));
        await Task.WhenAll(tasks);
    }
    
    public async Task SendToAll(string text, ParseMode? mode = null, CancellationToken token = default)
    {
        IEnumerable<long> users = _userRecordsDump.Execute().Select(user => user.Id);
        await Send(text, users, mode, token);
    }

    public async Task<Chat[]> GetChats(IEnumerable<long> chatIds)
    {
        IEnumerable<Task<Chat>> tasks = chatIds.Select(id => _client.GetChatAsync(id));
        return await Task.WhenAll(tasks);
    }
}