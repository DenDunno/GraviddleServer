using Telegram.Bot.Types;

namespace GraviddleServer.Code.Utils;

public static class ChatExtensions
{
    public static string GetFullName(this Chat chat)
    {
        return $"{chat.FirstName} {chat.LastName}";
    }
}