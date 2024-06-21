using GraviddleServer.Code.API;
using GraviddleServer.Code.Bot;
using GraviddleServer.Code.MsSqlRepositoryNM;
using GraviddleServer.Code.Parser;
using GraviddleServer.Code.Queries;
using TelegramBotNM.Bot;
using TelegramBotNM.Factory;
using TelegramBotNM.Notification;

namespace GraviddleServer.Code;

public abstract class CompositionRoot
{
    public static SecureData FetchSecureData()
    {
        return new SecureData(
            databaseConnectionString: @"",
            telegramBotToken: "",
            adminPassword: "");
    }
    
    public static TelegramBot CreateTelegramBot(MsSqlDatabaseBridge bridge, SecureData secureData)
    {
        ITelegramBotFactory telegramBotFactory = 
            new GraviddleAnalyticsBotFactory(bridge, secureData.TelegramBotToken);

        return telegramBotFactory.Create();
    }

    public static WebApplication CreateWebApplication(INotification notification, MsSqlDatabaseBridge bridge)
    {
        WebApplication app = WebApplication.Create();
        IRepository<LevelRecord> repository = new MsSqlRepository<LevelRecord>(bridge, new AnalyticsQueries(), new LevelRecordParser());
        Endpoints endpoints = new(notification, repository);
        
        app.MapGet("/", endpoints.Greet);
        app.MapGet("/post/{levelRecordJson}", endpoints.PostLevelResult);
        app.MapGet("/all", endpoints.GetAllRecords);

        return app;
    }
}