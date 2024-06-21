namespace TelegramBotNM.Repository.Commands.Contract;

public interface IRecordRemove<in TKey> 
{
    void Execute(TKey key);
}