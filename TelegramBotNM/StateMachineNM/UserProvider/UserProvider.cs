using TelegramBotNM.Repository.Commands.Contract;
using TelegramBotNM.UserNM;

namespace TelegramBotNM.StateMachineNM.UserProvider;

public class UserProvider : IUserProvider
{
    private readonly IRecordFetch<TelegramUser, long> _userFetch;

    public UserProvider(IRecordFetch<TelegramUser, long> userFetch)
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