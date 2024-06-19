using GraviddleServer.Code;
using GraviddleServer.Code.API;
using GraviddleServer.Code.Repository;
using GraviddleServer.Code.TelegramBotNM;

MsSqlDatabaseBridge bridge = new(@"Connection");
TelegramBot telegramBot = CompositionRoot.CreateTelegramBot(bridge);
INotification notification = new TelegramBotNotification(telegramBot.Bridge);
WebApplication app = CompositionRoot.CreateWebApplication(notification, bridge);

bridge.Open();
telegramBot.Run();
app.Run();
bridge.Dispose();