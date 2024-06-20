using GraviddleServer.Code.Repository;
using Telegram.Bot.Types;

namespace GraviddleServer.Code.TelegramBotNM.Commands.MessageCommands.ChatRepositoryCommands;

public class AddChatCommand : IMessageCommand
{
    private readonly IRepository<long> _chatsRepository;

    public AddChatCommand(IRepository<long> chatsRepository)
    {
        _chatsRepository = chatsRepository;
    }

    public Task Handle(Message message, CancellationToken token)
    {
        _chatsRepository.Add(message.Chat.Id);
        return Task.CompletedTask;
    }
}