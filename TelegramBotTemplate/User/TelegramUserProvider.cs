using Domain.Repository.Commands.Contract;
using Domain.Repository.Commands.Fetch;

namespace TelegramBotTemplate.User;

public class TelegramUserProvider : ITelegramUserProvider
{
    private readonly IRecordFetch<TelegramUser, long> _userFetch;

    public TelegramUserProvider(IRecordFetch<TelegramUser, long> userFetch)
    {
        _userFetch = userFetch;
    }

    public async Task<TelegramUser> Create(long chatId)
    {
        FetchResult<TelegramUser> result = await _userFetch.Execute(chatId);

        return result.Success ? result.Record : new TelegramUser(chatId, Role.User, 0);
    }
}