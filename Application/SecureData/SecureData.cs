namespace Application.SecureData;

public class SecureData
{
    public string DatabaseConnectionString { get; set; } = null!;
    public string TelegramBotToken { get; set; } = null!;
    public string AdminPassword { get; set; } = null!;
    public string ServerName { get; set; } = null!;
}