using TelegramBotNM.Repository.Commands.Contract;
using TelegramBotNM.User;

namespace TelegramBotNM.Repository;

public class TelegramUsersRepository
{
    public required IRecordFetch<TelegramUser, long> Fetch { get; init; }
    public required IRecordContains<long> Contains { get; init; }
    public required IRecordUpdate<TelegramUser> UpdateConversation { get; init; }
    public required IRecordUpdate<TelegramUser> UpdateRole { get; init; }
    public required IRecordRemove<long> Remove { get; init; }
    public required IRecordsDump<TelegramUser> Dump { get; init; }
    public required IRecordAdd<TelegramUser, long> Add { get; init; }
    public required IRecordsDump<long> AdminsDump { get; init; }
}