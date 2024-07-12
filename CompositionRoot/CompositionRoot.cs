using AnalyticsTelegramBot;
using AnalyticsTelegramBot.Messages.Formatter;
using Application;
using Application.Records;
using Application.Repository;
using Domain.Notification;
using Domain.Repository;
using GraviddleServer.Code;
using Microsoft.AspNetCore.Builder;
using Telegram.Bot.Types.Enums;
using TelegramBotTemplate.Bot;
using TelegramBotTemplate.Notification;

namespace CompositionRoot;

public static class CompositionRoot
{
    public static SecureData FetchSecureData()
    {
        string[] lines = File.ReadAllLines("password.txt");
        return new SecureData(lines[0], lines[1], lines[2]);
    }

    public static TelegramBot CreateTelegramBot(AnalyticsRepository repository, SecureData data, IDatabaseBridge bridge)
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