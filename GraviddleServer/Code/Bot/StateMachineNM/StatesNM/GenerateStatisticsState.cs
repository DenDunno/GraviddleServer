using GraviddleServer.Code.Bot.StateMachineNM.StatesNM.StatisticsGeneration;
using TelegramBotNM.Bot;
using TelegramBotNM.StateMachineNM.State;
using TelegramBotNM.User;

namespace GraviddleServer.Code.Bot.StateMachineNM.StatesNM;

public class GenerateStatisticsState : BaseState
{
    private readonly IStatisticsGeneration _statisticsGeneration;
    private readonly TelegramBotBridge _bridge;
    private readonly TelegramUser _user;

    public GenerateStatisticsState(TelegramBotBridge bridge, TelegramUser user, IStatisticsGeneration statisticsGeneration)
    {
        _statisticsGeneration = statisticsGeneration;
        _bridge = bridge;
        _user = user;
    }

    protected override async Task OnEnter(CancellationToken token)
    {
        Task<byte[]> deathPerLevel = _statisticsGeneration.DeathPerLevel.Evaluate();
        Task<byte[]> timePerLevel = _statisticsGeneration.TimePerLevel.Evaluate();
        Task<byte[]> starsPerLevel = _statisticsGeneration.StarsPerLevel.Evaluate();
        
        await Task.WhenAll(deathPerLevel, timePerLevel, starsPerLevel);
        
        await _bridge.SendPNGAlbum(_user.Id, new[]
        {
            deathPerLevel.Result,
            timePerLevel.Result,
            starsPerLevel.Result
        });
    }
}