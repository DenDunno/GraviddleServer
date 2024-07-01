using TelegramBotNM.Repository;

namespace GraviddleServer.Code.Repository;

public class Repositories
{
    public readonly TelegramUsersRepository TelegramUsers;
    public readonly AnalyticsRepository Analytics;

    public Repositories(IDatabaseBridge databaseBridge)
    {
        TelegramUsers = new TelegramUserRepositoryFactory(databaseBridge).Create();
        Analytics = new AnalyticsRepositoryFactory(databaseBridge).Create();
    }
}