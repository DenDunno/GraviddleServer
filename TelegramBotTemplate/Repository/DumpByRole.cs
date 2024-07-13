using Domain.Repository.Commands.Contract;
using TelegramBotTemplate.User;

namespace TelegramBotTemplate.Repository;

public class DumpByRole : IRecordsDump<long>
{
    private readonly IRecordsDump<TelegramUser> _usersDump;
    private readonly Role _role;

    public DumpByRole(IRecordsDump<TelegramUser> usersDump, Role role)
    {
        _usersDump = usersDump;
        _role = role;
    }

    public async Task<List<long>> Execute()
    {
        return (await _usersDump.Execute())
            .Where(user => user.Role == _role)
            .Select(user => user.Id)
            .ToList();
    }
}