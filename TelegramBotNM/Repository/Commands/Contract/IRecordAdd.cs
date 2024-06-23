namespace TelegramBotNM.Repository.Commands.Contract;

public interface IRecordAdd<in TRecord, TKey> where TRecord : IDatabaseModel<TKey>
{
    void Execute(TRecord element);
}