using TelegramBotNM.UserNM;

namespace TelegramBotNM.StateMachineNM.UserProvider;

public interface IUserProvider
{
    TelegramUser Create(long chatId);
}