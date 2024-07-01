namespace TelegramBotNM.User;

public interface ITelegramUserProvider
{
    TelegramUser Create(long chatId);
}