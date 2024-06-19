using GraviddleServer.Code.Repository;

namespace GraviddleServer.Code.TelegramBotNM.Commands.MessageCommands.ChatRepositoryCommands;

public class AddChatCommand : IMessageCommand
{
    private readonly IRepository<long> _chatsRepository;

    public AddChatCommand(IRepository<long> chatsRepository)
    {
        _chatsRepository = chatsRepository;
    }

    public Task Handle(long chatId, CancellationToken token)
    {
        _chatsRepository.Add(chatId);
        return Task.CompletedTask;
    }
}