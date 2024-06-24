using Telegram.Bot.Types.Enums;
using TelegramBotNM.Bot;
using TelegramBotNM.Repository;
using TelegramBotNM.StateMachineNM;
using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.Bot;

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
    public readonly IState ChatsDump;
    public readonly IState RecordsDump;
    public readonly IReadOnlyList<IState> All;
    
    public BotStates(TelegramUser user, UserRepository userRepository, TelegramBotBridge bridge)
    {
        Root = new RootState();
        Stop = new StopState();
        Admin = new EmptyState();
        User = new EmptyState();
        Start = new StartState(userRepository.Add, user);
        Authorization = new AuthorizationState(bridge, user.Id);
        InvalidPassword = new MessageState(bridge, user.Id, "Invalid password");
        SetAdminRole = new SetAdminRoleState(user, bridge, userRepository.UpdateRole);
        YouAreNotAdmin = new MessageState(bridge, user.Id, "You are not admin. Authorize first");
        YouAreAlreadyAdmin = new MessageState(bridge, user.Id, "You are already admin");
        ChatsDump = new MessageState(bridge, user.Id, new UserRecordsDumpMessage(userRepository.Dump));
        RecordsDump = new MessageState(bridge, user.Id, "Records dump");
        All = new []
        {
            Root, Stop, Admin, User, Start, Authorization, InvalidPassword, SetAdminRole, YouAreNotAdmin, 
            YouAreAlreadyAdmin, ChatsDump, RecordsDump
        };
    }
}