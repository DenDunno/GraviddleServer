using GraviddleServer.Code;
using GraviddleServer.Code.API;
using GraviddleServer.Code.MsSqlRepositoryNM;
using TelegramBotNM.Bot;
using TelegramBotNM.Notification;
using TelegramBotNM.Repository;

SecureData secureData = CompositionRoot.FetchSecureData();
IDatabaseBridge bridge = new MsSqlDatabaseBridge(secureData.DatabaseConnectionString);
Repositories repositories = new(bridge);
TelegramBot telegramBot = CompositionRoot.CreateTelegramBot(repositories, secureData);
INotification notification = new TelegramBotNotification(telegramBot.Bridge);
WebApplication app = CompositionRoot.CreateWebApplication(notification, repositories.Analytics);

bridge.Open();
telegramBot.Run();
app.Run();
bridge.Dispose();