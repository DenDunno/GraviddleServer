namespace TelegramBotNM.Repository.Commands.Contract;

public interface IRecordsDump<out T>
{
    IEnumerable<T> Execute();
}