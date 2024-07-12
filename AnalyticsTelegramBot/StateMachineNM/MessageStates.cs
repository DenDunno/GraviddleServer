using AnalyticsTelegramBot.Messages.TableMessages;
using TelegramBotTemplate.Bot;
using TelegramBotTemplate.StateMachineNM.State;
using TelegramBotTemplate.StateMachineNM.State.MessageState;

namespace AnalyticsTelegramBot.StateMachineNM;

public class MessageStates : States
{
    public readonly IState EnterUserId;
    public readonly IState RecordsDump;
    public readonly IState EnterPassword;
    public readonly IState GameUsersDump;
    public readonly IState YouAreNotAdmin;
    public readonly IState InvalidPlayerId;
    public readonly IState InvalidPassword;
    public readonly IState TelegramUsersDump;
    public readonly IState YouAreAlreadyAdmin;

    public MessageStates(TelegramBotBridge bridge, long chatId, Repositories repositories)
    {
        Add(EnterUserId = new MessageState(bridge, chatId, "Enter user id:"));
        Add(EnterPassword = new MessageState(bridge, chatId, "Enter password:"));
        Add(YouAreAlreadyAdmin = new MessageState(bridge, chatId, "You are already admin"));
        Add(InvalidPlayerId = new MessageState(bridge, chatId, "Invalid user id. Try again"));
        Add(InvalidPassword = new MessageState(bridge, chatId, "Invalid password. Try again"));
        Add(YouAreNotAdmin = new MessageState(bridge, chatId, "You are not admin. Authorize first"));
        Add(GameUsersDump = new MessageState(bridge, chatId, new GameUsersDump(repositories.Analytics.GameUsersDump)));
        Add(RecordsDump = new MessageState(bridge, chatId, new RecordsDumpMessage(repositories.Analytics.Dump)));
        Add(TelegramUsersDump = new MessageState(bridge, chatId,
            new TelegramUsersDumpMessage(repositories.TelegramUsers.Dump, bridge)));
    }
}