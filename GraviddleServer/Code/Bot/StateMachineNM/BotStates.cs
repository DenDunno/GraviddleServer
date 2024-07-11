using GraviddleServer.Code.Bot.StateMachineNM.StatesNM;
using GraviddleServer.Code.Bot.StateMachineNM.StatesNM.StatisticsGeneration;
using GraviddleServer.Code.Repository;
using TelegramBotNM.Bot;
using TelegramBotNM.StateMachineNM.State;
using TelegramBotNM.User;

namespace GraviddleServer.Code.Bot.StateMachineNM;

public class BotStates : States
{
    public readonly IState Root;
    public readonly IState Stop;
    public readonly IState User;
    public readonly IState Start;
    public readonly IState Admin;
    public readonly IState SetAdminRole;
    public readonly IState Authorization;
    public readonly IState CommandsListener;
    public readonly IState PlayerIdInput;
    public readonly IState GenerateAverageStatistics;
    public readonly IState GenerateStatisticsByPlayer;
    public readonly MessageStates Messages;

    public BotStates(TelegramUser user, string input, Repositories repositories, TelegramBotBridge bridge)
    {
        Add(Root = new EmptyState("Root", isPassive: false));
        Add(User = new EmptyState("User"));
        Add(Admin = new EmptyState("Admin"));
        Add(CommandsListener = new EmptyState("Command listener"));
        Add(PlayerIdInput = new EmptyState("Player id input", isPassive: false));
        Add(Authorization = new EmptyState("Authorization", isPassive: false));
        Add(Start = new StartState(bridge, user, repositories.TelegramUsers.Add));
        Add(Stop = new StopState(bridge, user.Id, repositories.TelegramUsers.Remove));
        Add(SetAdminRole = new SetAdminRoleState(user, bridge, repositories.TelegramUsers.UpdateRole));
        Add(Messages = new MessageStates(bridge, user.Id, repositories));
        Add(GenerateAverageStatistics = new GenerateStatisticsState(bridge, user,
            new QuickChartStatisticsGeneration(
                new ConstantProvider<string>("Average"), 
                repositories.Analytics.Dump)));

        Add(GenerateStatisticsByPlayer = new GenerateStatisticsState(bridge, user,
            new QuickChartStatisticsGeneration(
                new NameByIdProvider(input, repositories.Analytics.Fetch),
                new RecordsDumpById(input, repositories.Analytics.Dump))));
    }
}