using GraviddleServer.Code.Bot.StateMachineNM.StatesNM.StatisticsGeneration.AverageNM;
using GraviddleServer.Code.Repository.Records;
using TelegramBotNM.Repository.Commands.Contract;

namespace GraviddleServer.Code.Bot.StateMachineNM.StatesNM.StatisticsGeneration;

public class QuickChartStatisticsGeneration : IStatisticsGeneration
{
    private readonly IRecordsDump<LevelRecord> _recordsDump;
    private readonly IProvider<string> _prefixNameProvider;

    public QuickChartStatisticsGeneration(IProvider<string> prefixNameProvider, IRecordsDump<LevelRecord> recordsDump)
    {
        _prefixNameProvider = prefixNameProvider;
        _recordsDump = recordsDump;
    }

    public IPNGProvider TimePerLevel => CreateRequest(" time", "green", record => record.Time);
    public IPNGProvider DeathPerLevel => CreateRequest(" death", "black", record => record.DeathCount);
    public IPNGProvider StarsPerLevel => CreateRequest(" stars", "orange", record => record.Stars);

    private ChartRequest CreateRequest(string name, string color, Func<LevelRecord, double> observation)
    {
        string chartName = _prefixNameProvider.Provide() + name;
        AverageLevelsStatisticsQuery configProvider = new(_recordsDump, chartName, color, observation);

        return new ChartRequest(configProvider);
    }
}