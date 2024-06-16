using GraviddleServer.ChatRepository;
using GraviddleServer.TelegramBot;
using GraviddleServer.Level;
using GraviddleServer;

WebApplication app = WebApplication.Create(args);
TelegramBot telegramBot = new("Password", new SessionChatsRepository());
INotification notification = new TelegramBotNotification(telegramBot.Bridge);
Endpoints endpoints = new(notification);

app.MapGet("/", endpoints.Greet);
app.MapGet("/{levelResultJson}", endpoints.PostLevelResult);
app.MapGet("/all", endpoints.GetAllRecords);

telegramBot.Run();
app.Run();