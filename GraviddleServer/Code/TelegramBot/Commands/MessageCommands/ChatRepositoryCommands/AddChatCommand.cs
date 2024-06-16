using GraviddleServer.ChatRepository;
using GraviddleServer.TelegramBot.Commands;
namespace GraviddleServer.TelegramBot;

public class AddChatCommand : IMessageCommand
{
    private readonly IChatsRepository _chatsRepository;

    public AddChatCommand(IChatsRepository chatsRepository)
    {
        _chatsRepository = chatsRepository;
    }

    public Task Handle(long chatId, CancellationToken token)
    {
        _chatsRepository.TryAdd(chatId);
        return Task.CompletedTask;
    }
}