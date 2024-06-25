using GraviddleServer.Code.Bot.Messages;
using GraviddleServer.Code.Bot.StateMachineNM.States;
using TelegramBotNM.Bot;
using TelegramBotNM.StateMachineNM.State;
using TelegramBotNM.StateMachineNM.State.MessageState;
using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.Bot.StateMachineNM;

public class BotStates
{
    public readonly IState Root;
    public readonly IState Stop;
    public readonly IState Admin;
    public readonly IState User;
    public readonly IState Start;
    public readonly IState Authorization;
    public readonly IState InvalidPassword;
    public readonly IState SetAdminRole;
    public readonly IState YouAreNotAdmin;
    public readonly IState YouAreAlreadyAdmin;
    public readonly IState TelegramUsersDump;
    public readonly IState RecordsDump;
    public readonly IState CommandsListener;
    public readonly IState EnterPassword;
    public readonly IReadOnlyList<IState> All;

    public BotStates(TelegramUser user, Repositories repositories, TelegramBotBridge bridge)
    {
        All = new[]
        {
            Root = new RootState(),
            Stop = new StopState(),
            Admin = new EmptyState("Admin"),
            User = new EmptyState("User"),
            CommandsListener = new EmptyState("Command listener"),
            Start = new StartState(repositories.TelegramUsers.Add, user),
            Authorization = new AuthorizationState(),
            InvalidPassword = new MessageState(bridge, user.Id, "Invalid password. Try again"),
            SetAdminRole = new SetAdminRoleState(user, bridge, repositories.TelegramUsers.UpdateRole),
            YouAreNotAdmin = new MessageState(bridge, user.Id, "You are not admin. Authorize first"),
            YouAreAlreadyAdmin = new MessageState(bridge, user.Id, "You are already admin"),
            EnterPassword = new MessageState(bridge, user.Id, "Enter password:"),
            RecordsDump = new MessageState(bridge, user.Id, new RecordsDumpMessage(repositories.Analytics.Dump)),
            TelegramUsersDump = new MessageState(bridge, user.Id,
                new TelegramUsersDumpMessage(repositories.TelegramUsers.Dump, bridge)),
        };
    }
}