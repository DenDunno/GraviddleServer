using TelegramBotNM.Repository;

namespace TelegramBotNM.User;

public record TelegramUser(long Id, Role Role, int ConversationState) : IDatabaseModel<long>;