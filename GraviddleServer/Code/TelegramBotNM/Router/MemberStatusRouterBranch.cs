using GraviddleServer.Code.TelegramBotNM.Commands;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace GraviddleServer.Code.TelegramBotNM.Router;

public class MemberStatusRouterBranch : RouterBranch<long, ChatMemberStatus>
{
    public MemberStatusRouterBranch(IReadOnlyDictionary<ChatMemberStatus, IBotCommand<long>> commands) : base(commands)
    {
    }
    
    protected override UpdateType UpdateType => UpdateType.MyChatMember;

    protected override long FetchCommandInput(Update update) => update.MyChatMember!.Chat.Id;

    protected override ChatMemberStatus FetchCommandKey(Update update) => update.MyChatMember!.NewChatMember.Status;
}