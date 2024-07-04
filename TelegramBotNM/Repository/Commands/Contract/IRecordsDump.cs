namespace TelegramBotNM.Repository.Commands.Contract;

public interface IRecordsDump<T>
{
    List<T> Execute();
}