namespace GraviddleServer.Code.API;

public class SecureData
{
    public readonly string DatabaseConnectionString;
    public readonly string TelegramBotToken;
    public readonly string AdminPassword;

    public SecureData(string databaseConnectionString, string telegramBotToken, string adminPassword)
    {
        DatabaseConnectionString = databaseConnectionString;
        TelegramBotToken = telegramBotToken;
        AdminPassword = adminPassword;
    }
}