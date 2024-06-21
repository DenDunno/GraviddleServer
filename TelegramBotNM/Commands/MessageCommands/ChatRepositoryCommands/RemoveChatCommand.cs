using Telegram.Bot.Types;
using TelegramBotNM.Repository.Commands.Contract;

namespace TelegramBotNM.Commands.MessageCommands.ChatRepositoryCommands;

public class RemoveChatCommand : IMessageCommand, IBotCommand<long>
{
    private readonly IRecordRemove<long> _chatsRepository;

    public RemoveChatCommand(IRecordRemove<long> chatsRepository)
    {
        _chatsRepository = chatsRepository;
    }

    public Task Handle(Message message, CancellationToken token)
    {
        return Handle(message.Chat.Id, token);
    }

    public Task Handle(long chatId, CancellationToken token)
    {
        _chatsRepository.Execute(chatId);
        return Task.CompletedTask;
    }
}