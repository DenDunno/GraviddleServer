using GraviddleServer.Code;
using GraviddleServer.Code.API;
using GraviddleServer.Code.Repository;
using Telegram.Bot.Types.Enums;
using TelegramBotNM.Bot;
using TelegramBotNM.Notification;
using TelegramBotNM.Repository;

SecureData secureData = CompositionRoot.FetchSecureData();
IDatabaseBridge bridge = new MsSqlDatabaseBridge(secureData.DatabaseConnectionString);
Repositories repositories = new(bridge);
TelegramBot bot = CompositionRoot.CreateTelegramBot(repositories, secureData);
INotification<string> notification = new TelegramBotNotification(bot.Bridge, ParseMode.Html);
WebApplication app = CompositionRoot.CreateWebApplication(notification, bot.Logger, repositories.Analytics);

bridge.Open();
bot.Run();
app.Run();
bridge.Dispose();