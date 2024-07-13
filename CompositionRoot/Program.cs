using Application.Repository;
using Application.SecureData;
using CompositionRoot;
using Domain.Repository;
using Microsoft.AspNetCore.Builder;
using TelegramBotTemplate.Bot;

SecureData data = Composition.FetchSecureData();
DatabaseConnection dbConnection = new(new MsSqlDatabaseBridgeFactory(data.DatabaseConnectionString));
AnalyticsRepository analyticsRepository = new AnalyticsRepositoryFactory(dbConnection).Create();
TelegramBot bot = Composition.CreateTelegramBot(analyticsRepository, data, dbConnection);
WebApplication app = Composition.CreateWebApplication(bot, analyticsRepository);

bot.Run();
app.Run();