namespace TelegramBotNM.Repository.Commands.Contract;

public interface IRecordRemove<in T> 
{
    void Execute(T element);
}