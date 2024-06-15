using Telegram.Bot;
namespace GraviddleServer.TelegramBot;

public class TelegramBotStartup
{
    public void Run()
    {
        TelegramBotClient telegramBotClient = new("Password");
        TelegramBotBridge botBridge = new(telegramBotClient);
        TelegramBotRouter botRouter = new(botBridge);
        
        telegramBotClient.StartReceiving(botRouter.HandleInput, botRouter.HandleError);
    }
}