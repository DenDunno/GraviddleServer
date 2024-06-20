using GraviddleServer.Code.Repository;
using Telegram.Bot.Types;

namespace GraviddleServer.Code.TelegramBotNM.Commands.MessageCommands.ChatRepositoryCommands;

public class RemoveChatCommand : IMessageCommand, IBotCommand<long>
{
    private readonly IRepository<long> _chatsRepository;

    public RemoveChatCommand(IRepository<long> chatsRepository)
    {
        _chatsRepository = chatsRepository;
    }

    public Task Handle(Message message, CancellationToken token)
    {
        return Handle(message.Chat.Id, token);
    }

    public Task Handle(long chatId, CancellationToken token)
    {
        _chatsRepository.Remove(chatId);
        return Task.CompletedTask;
    }
}