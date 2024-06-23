using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotNM.Commands;

namespace TelegramBotNM.Router;

public class MemberStatusChangedBranch : RouterBranch<ChatMemberUpdated>
{
    private readonly IReadOnlyDictionary<ChatMemberStatus, IBotCommand<long>> _commands;

    public MemberStatusChangedBranch(IReadOnlyDictionary<ChatMemberStatus, IBotCommand<long>> commands)
    {
        _commands = commands;
    }
    
    protected override UpdateType UpdateType => UpdateType.MyChatMember;
    protected override ChatMemberUpdated GetKey(Update update) => update.MyChatMember!;

    protected override async Task OnHandle(ChatMemberUpdated message, CancellationToken cancellationToken)
    {
        await _commands[message.NewChatMember.Status].Handle(message.Chat.Id, cancellationToken);
    }
}