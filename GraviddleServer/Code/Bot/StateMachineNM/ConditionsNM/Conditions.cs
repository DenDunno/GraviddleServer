using GraviddleServer.Code.Repository;
using TelegramBotNM.StateMachineNM.TransitionNM.Condition;
using TelegramBotNM.User;

namespace GraviddleServer.Code.Bot.StateMachineNM.ConditionsNM;

public class Conditions
{
    public readonly ICondition Stop;
    public readonly ICondition Start;
    public readonly ICondition IsUser;
    public readonly ICondition IsAdmin;
    public readonly ICondition Authorize;
    public readonly ICondition RecordsDump;
    public readonly ICondition AdminCommand;
    public readonly ICondition ValidPlayerId;
    public readonly ICondition GameUsersDump;
    public readonly ICondition AnyCommandEntered;
    public readonly ICondition TelegramUsersDump;
    public readonly ICondition ValidAdminPassword;
    public readonly ICondition GenerateAverageStatistics;
    public readonly ICondition GenerateStatisticsByPlayer;

    public Conditions(TelegramUser user, string userInput, string adminPassword, Repositories repositories)
    {
        IsAdmin = new IsEqual<Role>(user.Role, Role.Admin);
        IsUser = new Not(IsAdmin);
        ValidAdminPassword = new IsEqual<string>(userInput, adminPassword);
        ValidPlayerId = new ValidPlayerIdCondition(userInput, repositories.Analytics.Contains);
        AnyCommandEntered = new Any(new[]
        {
            Stop = new IsEqual<string>(userInput, "/stop"),
            Start = new IsEqual<string>(userInput, "/start"),
            Authorize = new IsEqual<string>(userInput, "/authorize"),
            AdminCommand = new Any(new[]
            {
                RecordsDump = new IsEqual<string>(userInput, "/records_dump"),
                GameUsersDump = new IsEqual<string>(userInput, "/game_users_dump"),
                TelegramUsersDump = new IsEqual<string>(userInput, "/telegram_users_dump"), 
                GenerateAverageStatistics = new IsEqual<string>(userInput, "/generate_statistics"),
                GenerateStatisticsByPlayer = new IsEqual<string>(userInput, "/generate_statistics_by_player"),
            })
        });
    }
}