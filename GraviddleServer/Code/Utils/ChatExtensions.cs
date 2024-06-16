using Telegram.Bot.Types;

namespace GraviddleServer.Utils;

public static class ChatExtensions
{
    public static string GetFullName(this Chat chat)
    {
        return $"{chat.FirstName} {chat.LastName}";
    }
}