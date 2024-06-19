using GraviddleServer.Code.API;
using GraviddleServer.Code.Parser;
using GraviddleServer.Code.Queries;
using GraviddleServer.Code.Repository;
using GraviddleServer.Code.TelegramBotNM;

namespace GraviddleServer.Code;

public class CompositionRoot
{
    public static TelegramBot CreateTelegramBot(MsSqlDatabaseBridge bridge)
    {
        MsSqlRepository<long> msSqlRepository = new(bridge, new ChatQueries(), new ChatIdParser());
        IRepository<long> chatRepository = new SecureRepository<long>(msSqlRepository);

        return new TelegramBot("Token", chatRepository);;
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