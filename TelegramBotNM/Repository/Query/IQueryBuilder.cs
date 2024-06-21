namespace TelegramBotNM.Repository.Query;

public interface IQueryBuilder<in T>
{
    string Build(T element);
}