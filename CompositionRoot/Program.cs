using Application;
using Application.Repository;
using CompositionRoot;
using Domain.Repository;
using Microsoft.AspNetCore.Builder;
using TelegramBotTemplate.Bot;

SecureData secureData = Composition.FetchSecureData();
IDatabaseBridge bridge = new MsSqlDatabaseBridge(secureData.DatabaseConnectionString);
AnalyticsRepository analyticsRepository = new AnalyticsRepositoryFactory(bridge).Create();
TelegramBot bot = Composition.CreateTelegramBot(analyticsRepository, secureData, bridge);
WebApplication app = Composition.CreateWebApplication(bot, analyticsRepository);

bridge.Open();
bot.Run();
app.Run();
bridge.Dispose();