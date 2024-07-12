using Application.Repository;
using TelegramBotTemplate.StateMachineNM.TransitionNM.Condition;
using TelegramBotTemplate.User;

namespace AnalyticsTelegramBot.StateMachineNM.ConditionsNM;

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

    public Conditions(TelegramUser user, string input, string password, AnalyticsRepository repository)
    {
        IsAdmin = new IsEqual<Role>(user.Role, Role.Admin);
        IsUser = new Not(IsAdmin);
        ValidAdminPassword = new IsEqual<string>(input, password);
        ValidPlayerId = new ValidPlayerIdCondition(input, repository.Contains);
        AnyCommandEntered = new Any(new[]
        {
            Stop = new IsEqual<string>(input, "/stop"),
            Start = new IsEqual<string>(input, "/start"),
            Authorize = new IsEqual<string>(input, "/authorize"),
            AdminCommand = new Any(new[]
            {
                RecordsDump = new IsEqual<string>(input, "/records_dump"),
                GameUsersDump = new IsEqual<string>(input, "/game_users_dump"),
                TelegramUsersDump = new IsEqual<string>(input, "/telegram_users_dump"), 
                GenerateAverageStatistics = new IsEqual<string>(input, "/generate_statistics"),
                GenerateStatisticsByPlayer = new IsEqual<string>(input, "/generate_statistics_by_player"),
            })
        });
    }
}