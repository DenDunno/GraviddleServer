using GraviddleServer.Code.API;
using TelegramBotNM.Repository.Commands.Contract;

namespace GraviddleServer.Code.Bot.StateMachineNM.States.StatisticsGeneration;

public class QuickChartStatisticsGeneration : IStatisticsGeneration
{
    private readonly IRecordsDump<LevelRecord> _recordsDump;

    public QuickChartStatisticsGeneration(IRecordsDump<LevelRecord> recordsDump)
    {
        _recordsDump = recordsDump;
    }

    public IPNGProvider TimePerLevel => CreateRequest("Time", "green", record => record.Time);
    public IPNGProvider DeathPerLevel => CreateRequest("Death", "black", record => record.DeathCount);
    public IPNGProvider StarsPerLevel => CreateRequest("Stars", "orange", record => record.Stars);

    private ChartRequest CreateRequest(string name, string color, Func<LevelRecord, double> observation) => 
        new(new AverageLevelsStatisticsQuery(_recordsDump, name, color, observation));
} 