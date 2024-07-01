using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotNM.Bot;
using TelegramBotNM.Repository.Commands.Contract;
using TelegramBotNM.StateMachineNM.State.MessageState;
using TelegramBotNM.User;
using TelegramBotNM.Utils;

namespace GraviddleServer.Code.Bot.Messages;

public class TelegramUsersDumpMessage : ITelegramMessage
{
    private readonly IRecordsDump<TelegramUser> _userRecordsDump;
    private readonly TelegramBotBridge _bridge;

    public TelegramUsersDumpMessage(IRecordsDump<TelegramUser> userRecordsDump, TelegramBotBridge bridge)
    {
        _userRecordsDump = userRecordsDump;
        _bridge = bridge;
    }

    public ParseMode? Mode => ParseMode.Html;
    
    public async Task<string> GetText()
    {
        Table table = new("Num", "Name", "Role", "Chat id");
        IList<TelegramUser> users = _userRecordsDump.Execute();
        Chat[] chats = await _bridge.GetChats(users.Select(user => user.Id));

        for (int i = 0; i < users.Count; ++i)
        {
            table.Add(i + 1, chats[i].GetFullName(), users[i].Role, users[i].Id);
        }
        
        return table.Build();
    }
}