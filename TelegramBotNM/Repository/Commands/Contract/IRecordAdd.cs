namespace TelegramBotNM.Repository.Commands.Contract;

public interface IRecordAdd<in T>
{
    void Execute(T element);
}