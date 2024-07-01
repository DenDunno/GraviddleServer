using TelegramBotNM.StateMachineNM.TransitionNM.Condition;
using TelegramBotNM.User;

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

    public Conditions(TelegramUser user, string userInput, string adminPassword)
    {
        IsAdmin = new IsEqual<Role>(user.Role, Role.Admin);
        IsUser = new Not(IsAdmin);
        Start = new IsEqual<string>(userInput, "/start");
        Stop = new IsEqual<string>(userInput, "/stop");
        Authorize = new IsEqual<string>(userInput, "/authorize");
        TelegramUsersDump = new IsEqual<string>(userInput, "/telegram_users_dump");
        RecordsDump = new IsEqual<string>(userInput, "/records_dump");
        ValidAdminPassword = new IsEqual<string>(userInput, adminPassword);
        AnyCommandEntered = new Any(TelegramUsersDump, RecordsDump, Stop, Start, Authorize);
        BadAdminPassword = new Not(ValidAdminPassword);
        RestrictedCommand = new Any(TelegramUsersDump, RecordsDump);
    }
}