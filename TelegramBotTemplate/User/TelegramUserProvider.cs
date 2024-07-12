using Domain.Repository.Commands.Contract;

namespace TelegramBotTemplate.User;

public class TelegramUserProvider : ITelegramUserProvider
{
    private readonly IRecordFetch<TelegramUser, long> _userFetch;

    public TelegramUserProvider(IRecordFetch<TelegramUser, long> userFetch)
    {
        _userFetch = userFetch;
    }

    public TelegramUser Create(long chatId)
    {
        if (_userFetch.TryExecute(chatId, out TelegramUser? user) == false)
        {
            user = new TelegramUser(chatId, Role.User, 0);
        }

        return user;
    }
}