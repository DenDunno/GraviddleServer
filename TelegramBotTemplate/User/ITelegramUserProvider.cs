namespace TelegramBotTemplate.User;

public interface ITelegramUserProvider
{
    TelegramUser Create(long chatId);
}