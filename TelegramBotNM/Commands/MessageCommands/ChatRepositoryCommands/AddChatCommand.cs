using Telegram.Bot.Types;
using TelegramBotNM.Repository.Commands.Contract;

namespace TelegramBotNM.Commands.MessageCommands.ChatRepositoryCommands;

public class AddChatCommand : IMessageCommand
{
    private readonly IRecordAdd<long> _chatsRepository;

    public AddChatCommand(IRecordAdd<long> chatsRepository)
    {
        _chatsRepository = chatsRepository;
    }

    public Task Handle(Message message, CancellationToken token)
    {
        _chatsRepository.Execute(message.Chat.Id);
        return Task.CompletedTask;
    }
}