using Telegram.Bot.Types.Enums;
namespace GraviddleServer.TelegramBot.MemberStatusCommands;

public interface IMemberStatusCommand : IBotCommand<ChatMemberStatus>
{
}