namespace GraviddleServer.Code.API;

public class SecureData
{
    public readonly string DatabaseConnectionString;
    public readonly string TelegramBotToken;
    public readonly string AdminPassword;

    public SecureData(string telegramBotToken, string adminPassword, string databaseConnectionString)
    {
        DatabaseConnectionString = databaseConnectionString;
        TelegramBotToken = telegramBotToken;
        AdminPassword = adminPassword;
    }
}