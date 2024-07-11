using GraviddleServer.Code.API;
using GraviddleServer.Code.Bot;
using GraviddleServer.Code.Bot.Messages.Formatter;
using GraviddleServer.Code.Repository;
using GraviddleServer.Code.Repository.Records;
using Telegram.Bot.Types.Enums;
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
        ITelegramBotFactory telegramBotFactory = new TelegramBotFactory(repositories, data);
        return telegramBotFactory.Create();
    }

    public static WebApplication CreateWebApplication(TelegramBot bot, AnalyticsRepository repository)
    {
        TelegramBotTextNotification textNotification = new(bot.Bridge, ParseMode.Html);
        TelegramBotImageNotification imageNotification = new(bot.Bridge, ParseMode.Html);
        
        WebApplication app = WebApplication.Create();
        Endpoints endpoints = new(
            repository.Add,
            repository.Dump,
            new FormattedStringNotification<LevelRecord>(textNotification, new LevelRecordFormatter()),
            new DeathRecordNotification(imageNotification, new DeathRecordFormatter()));

        app.UseMiddleware<ExceptionMiddleware>(bot.Logger);
        app.MapGet("/", endpoints.Greet);
        app.MapGet("/all", endpoints.GetAllRecords);
        app.MapPost("/postLevelRecord", endpoints.PostLevelRecord);
        app.MapPost("/postDeathRecord", endpoints.PostDeathRecord);
        
        return app;
    }
}