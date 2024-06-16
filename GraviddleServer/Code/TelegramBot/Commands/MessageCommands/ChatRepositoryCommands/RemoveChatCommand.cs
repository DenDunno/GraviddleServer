using GraviddleServer.ChatRepository;
using GraviddleServer.TelegramBot.Commands;

namespace GraviddleServer.TelegramBot;

public class RemoveChatCommand : IMessageCommand
{
    private readonly IChatsRepository _chatsRepository;

    public RemoveChatCommand(IChatsRepository chatsRepository)
    {
        _chatsRepository = chatsRepository;
    }

    public Task Handle(long chatId, CancellationToken token)
    {
        _chatsRepository.TryRemove(chatId);
        return Task.CompletedTask;
    }
}