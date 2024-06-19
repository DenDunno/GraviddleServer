
using GraviddleServer.ChatRepository;

namespace GraviddleServer.Code.TelegramBot.Commands.MessageCommands.ChatRepositoryCommands;

public class RemoveChatCommand : IMessageCommand
{
    private readonly IChatsRepository _repository;

    public RemoveChatCommand(IChatsRepository repository)
    {
        _repository = repository;
    }

    public Task Handle(long chatId, CancellationToken token)
    {
        _repository.TryRemove(chatId);
        return Task.CompletedTask;
    }
}