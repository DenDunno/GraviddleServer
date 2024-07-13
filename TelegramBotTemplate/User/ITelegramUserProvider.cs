namespace TelegramBotTemplate.User;

public interface ITelegramUserProvider
{
    Task<TelegramUser> Create(long chatId);
}