using GraviddleServer.Code.API;
using GraviddleServer.Code.Bot;
using GraviddleServer.Code.Formatter;
using GraviddleServer.Code.Repository;
using TelegramBotNM.Bot;
using TelegramBotNM.Logger;
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
        ITelegramBotFactory telegramBotFactory = new TelegramBotFactory(repositories, data);
        return telegramBotFactory.Create();
    }

    public static WebApplication CreateWebApplication(INotification<string> notification, IMessageLogger logger, AnalyticsRepository repository)
    {
        WebApplication app = WebApplication.Create();
        Endpoints endpoints = new(
            repository.Add,
            repository.Dump,
            new FormattedStringNotification<LevelRecord>(notification, new LevelRecordFormatter()),
            new FormattedStringNotification<DeathRecord>(notification, new DeathRecordFormatter()));

        app.UseMiddleware<ExceptionMiddleware>(logger);
        app.MapGet("/", endpoints.Greet);
        app.MapGet("/all", endpoints.GetAllRecords);
        app.MapPost("/postLevelRecord", endpoints.PostLevelRecord);
        app.MapPost("/postDeathRecord", endpoints.PostDeathRecord);
        

        return app;
    }
}