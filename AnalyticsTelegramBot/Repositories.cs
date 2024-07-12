using Application.Repository;
using Domain.Repository;
using TelegramBotTemplate.Repository;

namespace AnalyticsTelegramBot;

public class Repositories
{
    public readonly TelegramUsersRepository TelegramUsers;
    public readonly AnalyticsRepository Analytics;

    public Repositories(AnalyticsRepository analytics, IDatabaseBridge databaseBridge)
    {
        TelegramUsers = new TelegramUserRepositoryFactory(databaseBridge).Create();
        Analytics = analytics;
    }
}