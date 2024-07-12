using Domain.Repository;

namespace TelegramBotTemplate.User;

public record TelegramUser(long Id, Role Role, int ConversationState) : IDatabaseModel<long>;