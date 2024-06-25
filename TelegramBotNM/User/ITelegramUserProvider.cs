using TelegramBotNM.UserNM;

namespace TelegramBotNM.StateMachineNM.UserProvider;

public interface ITelegramUserProvider
{
    TelegramUser Create(long chatId);
}