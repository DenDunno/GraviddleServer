using AnalyticsTelegramBot;
using AnalyticsTelegramBot.Messages.Formatter;
using Application;
using Application.Records;
using Application.Repository;
using Application.SecureData;
using Coravel;
using Domain.Notification;
using Domain.Repository;
using GraviddleServer.Code;
using GraviddleServer.Code.Refresh;
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

    public static WebApplication CreateWebApplication(TelegramBot bot, AnalyticsRepository repository, string serverName)
    {
        WebApplication app = BuildApp();
        ScheduleServerRefresh(bot, app, serverName);
        SetupEndpoints(bot, repository, app);
        
        return app;
    }

    private static WebApplication BuildApp()
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        builder.Services.AddScheduler();
        return builder.Build();
    }

    private static void ScheduleServerRefresh(TelegramBot bot, WebApplication app, string serverName)
    {
        ServerRefresh serverRefresh = new(bot.Logger, serverName);
        ISchedulerBuilder schedulerBuilder = new FifteenMinutesSchedule(serverRefresh);
        app.Services.UseScheduler(scheduler => schedulerBuilder.Build(scheduler));
    }

    private static void SetupEndpoints(TelegramBot bot, AnalyticsRepository repository, WebApplication app)
    {
        TelegramBotTextNotification textNotification = new(bot.Bridge, ParseMode.Html);
        TelegramBotImageNotification imageNotification = new(bot.Bridge, ParseMode.Html);
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
    }
}