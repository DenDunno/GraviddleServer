namespace TelegramBotNM.Repository;

public interface IDatabaseModel<out TKey>
{
    TKey Id { get; }
}