using GraviddleServer.TelegramBot;
using GraviddleServer;

WebApplication app = WebApplication.Create(args);
TelegramBotStartup telegramBotStartup = new();
Endpoints endpoints = new();

app.MapGet("/", endpoints.Greet);
app.MapPost("/{input}", endpoints.PostLevelResult);
app.MapGet("/all", endpoints.GetAllRecords);

telegramBotStartup.Run();
app.Run();