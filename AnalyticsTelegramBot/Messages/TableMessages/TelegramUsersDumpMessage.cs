using Domain.Repository.Commands.Contract;
using Telegram.Bot.Types;
using TelegramBotTemplate.Bot;
using TelegramBotTemplate.User;
using TelegramBotTemplate.Utils;

namespace AnalyticsTelegramBot.Messages.TableMessages;

public class TelegramUsersDumpMessage : TableMessage
{
    private readonly IRecordsDump<TelegramUser> _userRecordsDump;
    private readonly TelegramBotBridge _bridge;

    public TelegramUsersDumpMessage(IRecordsDump<TelegramUser> userRecordsDump, TelegramBotBridge bridge)
    {
        _userRecordsDump = userRecordsDump;
        _bridge = bridge;
    }

    protected override string[] Columns => new[] { "Num", "Name", "Role", "State id", "Chat id" };

    protected override async Task WriteRaws(List<object[]> raws)
    {
        IList<TelegramUser> users = await _userRecordsDump.Execute();
        Chat[] chats = await _bridge.GetChats(users.Select(user => user.Id));

        for (int i = 0; i < users.Count; ++i)
        {
            raws.Add(new object[]
                { i + 1, chats[i].GetFullName(), users[i].Role, users[i].ConversationState, users[i].Id });
        }
    }
}