namespace TelegramBotNM.Repository.Commands.Contract;

public interface IRecordFetch<out TRecord, in TKey>
{
    TRecord Execute(TKey key);
}