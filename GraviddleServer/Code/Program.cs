using GraviddleServer.Code;
using GraviddleServer.Code.API;
using GraviddleServer.Code.Repository;
using GraviddleServer.Code.TelegramBotNM;

SecureData secureData = CompositionRoot.FetchSecureData();
MsSqlDatabaseBridge bridge = new(secureData.DatabaseConnectionString);
TelegramBot telegramBot = CompositionRoot.CreateTelegramBot(bridge, secureData);
INotification notification = new TelegramBotNotification(telegramBot.Bridge);
WebApplication app = CompositionRoot.CreateWebApplication(notification, bridge);

bridge.Open();
telegramBot.Run();
app.Run();
bridge.Dispose();