using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotNM.Router.Commands;

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
        if (_commands.TryGetValue(message.NewChatMember.Status, out IBotCommand<long>? command))
        {
            await command.Handle(message.Chat.Id, cancellationToken);    
        }
    }
}