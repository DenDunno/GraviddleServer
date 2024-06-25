using GraviddleServer.Code.API;
using GraviddleServer.Code.Bot;
using GraviddleServer.Code.MsSqlRepositoryNM;
using TelegramBotNM.Bot;
using TelegramBotNM.Notification;

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
    
    public static TelegramBot CreateTelegramBot(Repositories repositories, SecureData data)
    {
        ITelegramBotFactory telegramBotFactory = new TelegramBotFactory(repositories, data.TelegramBotToken);
        return telegramBotFactory.Create();
    }

    public static WebApplication CreateWebApplication(INotification notification, AnalyticsRepository analyticsRepository)
    {
        WebApplication app = WebApplication.Create();
        Endpoints endpoints = new(notification, analyticsRepository.Add, analyticsRepository.Dump);
        
        app.MapGet("/", endpoints.Greet);
        app.MapGet("/post/{levelRecordJson}", endpoints.PostLevelResult);
        app.MapGet("/all", endpoints.GetAllRecords);

        return app;
    }
}