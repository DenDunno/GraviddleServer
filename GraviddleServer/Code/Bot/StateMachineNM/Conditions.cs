using TelegramBotNM.StateMachineNM.TransitionNM.Condition;
using TelegramBotNM.UserNM;

namespace GraviddleServer.Code.Bot.StateMachineNM;

public class Conditions
{
    public readonly ICondition IsUser;
    public readonly ICondition IsAdmin;
    public readonly ICondition Start;
    public readonly ICondition Stop;
    public readonly ICondition Authorize;
    public readonly ICondition TelegramUsersDump;
    public readonly ICondition RecordsDump;
    public readonly ICondition ValidAdminPassword;
    public readonly ICondition BadAdminPassword;
    public readonly ICondition RestrictedCommand;
    public readonly ICondition AnyCommandEntered;

    public Conditions(TelegramUser user, string input)
    {
        IsAdmin = new IsEqual<Role>(user.Role, Role.Admin);
        IsUser = new Not(IsAdmin);
        Start = new IsEqual<string>(input, "/start");
        Stop = new IsEqual<string>(input, "/stop");
        Authorize = new IsEqual<string>(input, "/authorize");
        TelegramUsersDump = new IsEqual<string>(input, "/telegram_users_dump");
        RecordsDump = new IsEqual<string>(input, "/records_dump");
        ValidAdminPassword = new IsEqual<string>(input, "1223");
        AnyCommandEntered = new Any(TelegramUsersDump, RecordsDump, Stop, Start, Authorize);
        BadAdminPassword = new Not(ValidAdminPassword);
        RestrictedCommand = new Any(TelegramUsersDump, RecordsDump);
    }
}