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
        string[] lines = File.ReadAllLines("password.txt");
        return new SecureData(lines[0], lines[1], lines[2]);
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