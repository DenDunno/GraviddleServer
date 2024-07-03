using GraviddleServer.Code.API;
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

    public async Task SendText(ITelegramMessage message, long chatId, CancellationToken token = default)
    {
        await SendText(await message.GetText(), chatId, message.Mode, token);
    }

    private async Task SendText(string text, long chatId, ParseMode? mode = null, CancellationToken token = default)
    {
        await _client.SendTextMessageAsync(chatId, text, parseMode: mode, cancellationToken: token);
    }

    public async Task SendText(string text, IEnumerable<long> chats, ParseMode? mode = null,
        CancellationToken token = default)
    {
        IEnumerable<Task> tasks = chats.Select(chatId => SendText(text, chatId, mode, token));
        await Task.WhenAll(tasks);
    }

    public async Task SendTextToAll(string text, ParseMode? mode = null, CancellationToken token = default)
    {
        await SendText(text, GetAllUsers(), mode, token);
    }

    public async Task SendImage(long chatId, ImageMessageData data, ParseMode? parseMode = null)
    {
        using MemoryStream memoryStream = new(data.Image);
        InputFileStream imageStream = new(memoryStream, "image.png");
        await _client.SendPhotoAsync(chatId, imageStream, caption: data.Message, parseMode: parseMode);
    }

    public async Task SendImageMessageToAll(ImageMessageData data, ParseMode? parseMode = null)
    {
        IEnumerable<Task> tasks = GetAllUsers().Select(chatId => SendImage(chatId, data, parseMode));
        await Task.WhenAll(tasks);
    }

    public async Task<Chat[]> GetChats(IEnumerable<long> chatIds)
    {
        IEnumerable<Task<Chat>> tasks = chatIds.Select(id => _client.GetChatAsync(id));
        return await Task.WhenAll(tasks);
    }

    private IEnumerable<long> GetAllUsers()
    {
        return _userRecordsDump.Execute().Select(user => user.Id);
    }
}