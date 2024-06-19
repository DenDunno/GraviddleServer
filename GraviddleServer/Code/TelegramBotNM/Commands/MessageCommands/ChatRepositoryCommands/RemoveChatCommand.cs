
using GraviddleServer.Code.Repository;

namespace GraviddleServer.Code.TelegramBotNM.Commands.MessageCommands.ChatRepositoryCommands;

public class RemoveChatCommand : IMessageCommand
{
    private readonly IRepository<long> _chatsRepository;

    public RemoveChatCommand(IRepository<long> chatsRepository)
    {
        _chatsRepository = chatsRepository;
    }

    public Task Handle(long chatId, CancellationToken token)
    {
        _chatsRepository.Remove(chatId);
        return Task.CompletedTask;
    }
}