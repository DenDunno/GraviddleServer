using GraviddleServer.ChatRepository;

namespace GraviddleServer.Code.TelegramBot.Commands.MessageCommands.ChatRepositoryCommands;

public class AddChatCommand : IMessageCommand
{
    private readonly IChatsRepository _repository;

    public AddChatCommand(IChatsRepository repository)
    {
        _repository = repository;
    }

    public Task Handle(long chatId, CancellationToken token)
    {
        _repository.TryAdd(chatId);
        return Task.CompletedTask;
    }
}