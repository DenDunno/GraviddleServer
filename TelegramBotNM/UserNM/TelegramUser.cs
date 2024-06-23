using TelegramBotNM.Repository;

namespace TelegramBotNM.UserNM;

public record TelegramUser(long Id, Role Role, int ConversationState) : IDatabaseModel<long>;