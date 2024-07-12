using Application;
using Application.Repository;
using Domain.Repository;
using Microsoft.AspNetCore.Builder;
using TelegramBotTemplate.Bot;

SecureData secureData = CompositionRoot.CompositionRoot.FetchSecureData();
IDatabaseBridge bridge = new MsSqlDatabaseBridge(secureData.DatabaseConnectionString);
AnalyticsRepository analyticsRepository = new AnalyticsRepositoryFactory(bridge).Create();
TelegramBot bot = CompositionRoot.CompositionRoot.CreateTelegramBot(analyticsRepository, secureData, bridge);
WebApplication app = CompositionRoot.CompositionRoot.CreateWebApplication(bot, analyticsRepository);

bridge.Open();
bot.Run();
app.Run();
bridge.Dispose();