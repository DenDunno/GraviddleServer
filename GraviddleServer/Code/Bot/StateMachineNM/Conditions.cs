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
    public readonly ICondition AdminCommand;
    public readonly ICondition AnyCommandEntered;
    public readonly ICondition GenerateLevelStatistics;

    public Conditions(TelegramUser user, string userInput, string adminPassword)
    {
        IsAdmin = new IsEqual<Role>(user.Role, Role.Admin);
        IsUser = new Not(IsAdmin);
        Start = new IsEqual<string>(userInput, "/start");
        Stop = new IsEqual<string>(userInput, "/stop");
        Authorize = new IsEqual<string>(userInput, "/authorize");
        TelegramUsersDump = new IsEqual<string>(userInput, "/telegram_users_dump");
        RecordsDump = new IsEqual<string>(userInput, "/records_dump");
        GenerateLevelStatistics = new IsEqual<string>(userInput, "/generate_level_statistics");
        ValidAdminPassword = new IsEqual<string>(userInput, adminPassword);
        BadAdminPassword = new Not(ValidAdminPassword);
        AnyCommandEntered = new Any(new[]
        {
            Stop,
            Start,
            Authorize,
            AdminCommand = new Any(new[] { TelegramUsersDump, RecordsDump, GenerateLevelStatistics })
        });
    }
}