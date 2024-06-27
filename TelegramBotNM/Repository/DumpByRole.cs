using TelegramBotNM.Repository.Commands.Contract;
using TelegramBotNM.UserNM;

namespace TelegramBotNM.Repository;

public class DumpByRole : IRecordsDump<long>
{
    private readonly IRecordsDump<TelegramUser> _usersDump;
    private readonly Role _role;

    public DumpByRole(IRecordsDump<TelegramUser> usersDump, Role role)
    {
        _usersDump = usersDump;
        _role = role;
    }

    public IList<long> Execute()
    {
        return _usersDump.Execute()
            .Where(user => user.Role == _role)
            .Select(user => user.Id)
            .ToList();
    }
}