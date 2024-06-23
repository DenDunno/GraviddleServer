using GraviddleServer.Code.API;
using GraviddleServer.Code.Bot;
using GraviddleServer.Code.MsSqlRepositoryNM;
using GraviddleServer.Code.Parser;
using TelegramBotNM.Bot;
using TelegramBotNM.Notification;
using TelegramBotNM.Repository;

namespace GraviddleServer.Code;

public static class CompositionRoot
{
    public static SecureData FetchSecureData()
    {
        return new SecureData(
            databaseConnectionString: @"",
            telegramBotToken: "",
            adminPassword: "");
    }
    
    public static TelegramBot CreateTelegramBot(IDatabaseBridge bridge, SecureData secureData)
    {
        ITelegramBotFactory telegramBotFactory = new TelegramBotFactory(bridge, secureData.TelegramBotToken);
        return telegramBotFactory.Create();
    }

    public static WebApplication CreateWebApplication(INotification notification, IDatabaseBridge bridge)
    {
        WebApplication app = WebApplication.Create();
        Repository<LevelRecord, string> repository = new AnalyticsRepositoryFactory(bridge).Create();
        Endpoints endpoints = new(notification, repository.Add, repository.Dump);
        
        app.MapGet("/", endpoints.Greet);
        app.MapGet("/post/{levelRecordJson}", endpoints.PostLevelResult);
        app.MapGet("/all", endpoints.GetAllRecords);

        return app;
    }
}