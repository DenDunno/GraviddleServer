using AnalyticsTelegramBot;
using AnalyticsTelegramBot.Messages.Formatter;
using Application;
using Application.Records;
using Application.Repository;
using Application.SecureData;
using Domain.Notification;
using Domain.Repository;
using GraviddleServer.Code;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using Telegram.Bot.Types.Enums;
using TelegramBotTemplate.Bot;
using TelegramBotTemplate.Notification;

namespace CompositionRoot;

public static class Composition
{
    public static SecureData FetchSecureData()
    {
        string text = File.ReadAllText("password.env");
        IEncryption encryption = new AesEncryption();
        string json = encryption.Decrypt(text, key: "770A8A65DA156D24EE2A093277530142");
        return JsonConvert.DeserializeObject<SecureData>(json)!;
    }

    public static TelegramBot CreateTelegramBot(AnalyticsRepository repository, SecureData data, DatabaseConnection bridge)
    {
        Repositories repositories = new(repository, bridge);
        ITelegramBotFactory telegramBotFactory = new TelegramBotFactory(data, repositories);
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