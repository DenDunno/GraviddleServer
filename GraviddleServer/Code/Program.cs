using GraviddleServer.Code;
using GraviddleServer.Code.API;
using GraviddleServer.Code.MsSqlRepositoryNM;
using TelegramBotNM.Bot;
using TelegramBotNM.Notification;
using TelegramBotNM.Repository;

SecureData secureData = CompositionRoot.FetchSecureData();
IDatabaseBridge bridge = new MsSqlDatabaseBridge(secureData.DatabaseConnectionString);
TelegramBot telegramBot = CompositionRoot.CreateTelegramBot(bridge, secureData);
INotification notification = new TelegramBotNotification(telegramBot.Bridge);
WebApplication app = CompositionRoot.CreateWebApplication(notification, bridge);

bridge.Open();
telegramBot.Run();
app.Run();
bridge.Dispose();