using GraviddleServer.ChatRepository;
using GraviddleServer.Code;
using GraviddleServer.Code.GraviddleDatabase;
using GraviddleServer.Code.Level;
using GraviddleServer.Code.TelegramBot;

WebApplication app = WebApplication.Create(args);
TelegramBot telegramBot = new("Password", new SessionChatsRepository());
INotification notification = new TelegramBotNotification(telegramBot.Bridge);
Endpoints endpoints = new(notification);
Database database = new(@"ConnectionString");

app.MapGet("/", endpoints.Greet);
app.MapGet("/{levelResultJson}", endpoints.PostLevelResult);
app.MapGet("/all", endpoints.GetAllRecords);

database.Open();
telegramBot.Run();
app.Run();
database.Dispose();