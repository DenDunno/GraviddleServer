using TelegramBotNM.StateMachineNM.TransitionNM.Condition;
using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.Bot;

public class Conditions
{
    public readonly ICondition IsUser;
    public readonly ICondition IsAdmin;
    public readonly ICondition Start;
    public readonly ICondition Stop;
    public readonly ICondition Authorize;
    public readonly ICondition ChatsDump;
    public readonly ICondition RecordsDump;
    public readonly ICondition ValidAdminPassword;
    public readonly ICondition BadAdminPassword;
    public readonly ICondition RestrictedCommand;

    public Conditions(TelegramUser user, string input)
    {
        IsAdmin = new IsEqual<Role>(user.Role, Role.Admin);
        IsUser = new Not(IsAdmin);
        Start = new IsEqual<string>(input, "/start");
        Stop = new IsEqual<string>(input, "/stop");
        Authorize = new IsEqual<string>(input, "/authorize");
        ChatsDump = new IsEqual<string>(input, "/chatsdump");
        RecordsDump = new IsEqual<string>(input, "/recordsdump");
        ValidAdminPassword = new IsEqual<string>(input, "1223");
        BadAdminPassword = new Not(ValidAdminPassword);
        RestrictedCommand = new Any(ChatsDump, RecordsDump);
    }
}