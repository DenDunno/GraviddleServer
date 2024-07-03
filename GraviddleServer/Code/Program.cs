using GraviddleServer.Code;
using GraviddleServer.Code.API;
using GraviddleServer.Code.Repository;
using TelegramBotNM.Bot;
using TelegramBotNM.Repository;

SecureData secureData = CompositionRoot.FetchSecureData();
IDatabaseBridge bridge = new MsSqlDatabaseBridge(secureData.DatabaseConnectionString);
Repositories repositories = new(bridge);
TelegramBot bot = CompositionRoot.CreateTelegramBot(repositories, secureData);
WebApplication app = CompositionRoot.CreateWebApplication(bot, repositories.Analytics);

bridge.Open();
bot.Run();
app.Run();
bridge.Dispose();