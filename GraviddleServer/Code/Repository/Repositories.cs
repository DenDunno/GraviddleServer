using GraviddleServer.Code.MsSqlRepositoryNM;
using TelegramBotNM.Repository;

namespace GraviddleServer.Code;

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