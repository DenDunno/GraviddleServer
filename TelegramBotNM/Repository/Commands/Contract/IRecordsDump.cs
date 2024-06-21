namespace TelegramBotNM.Repository.Commands.Contract;

public interface IRecordsDump<T>
{
    IList<T> Execute();
}