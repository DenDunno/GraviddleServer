using GraviddleServer.Code.API;
using TelegramBotNM.Repository.Commands.Contract;

namespace GraviddleServer.Code.Bot.StateMachineNM.States;

public class AverageLevelsStatisticsQuery : IConfigProvider
{
    private readonly IRecordsDump<LevelRecord> _recordsDump;
    private readonly Func<LevelRecord, double> _observation;
    private readonly string _statisticsName;

    public AverageLevelsStatisticsQuery(IRecordsDump<LevelRecord> recordsDump, string statisticsName, Func<LevelRecord, double> observation)
    {
        _statisticsName = statisticsName;
        _recordsDump = recordsDump;
        _observation = observation;
    }

    public ChartConfig GetConfig()
    {
        List<LevelRecord> records = _recordsDump.Execute();
        Dictionary<int, string> levelNamesMap = GetLevelNamesMap(records);
        KeyValuePair<int, Average>[] averageTimePerLevel = GetAverageTimePerLevel(records);

        string[] labels = averageTimePerLevel.Select(x => levelNamesMap[x.Key]).ToArray();
        double[] data = averageTimePerLevel.Select(x => x.Value.Value).ToArray();
        
        return BuildConfig(labels, data);
    }

    private KeyValuePair<int, Average>[] GetAverageTimePerLevel(List<LevelRecord> records)
    {
        Dictionary<int, Average> averageTime = new();
        
        foreach (LevelRecord levelRecord in records)
        {
            averageTime.TryAdd(levelRecord.LevelIndex, new Average());
            double observedValue = _observation(levelRecord);
            averageTime[levelRecord.LevelIndex].Add(observedValue);
        }

        KeyValuePair<int, Average>[] keyValuePairs = averageTime.ToArray();
        Array.Sort(keyValuePairs, (x, y) => x.Key.CompareTo(y.Key));
        return keyValuePairs;
    }

    private Dictionary<int, string> GetLevelNamesMap(List<LevelRecord> records)
    {
        Dictionary<int, string> levelName = new();

        foreach (LevelRecord record in records)
        {
            levelName[record.LevelIndex] = record.Level;
        }

        return levelName;
    }

    private ChartConfig BuildConfig(string[] labels, double[] data)
    {
        return new ChartConfig()
        {
            Type = "line",
            Data = new ChartData()
            {
                Labels = labels,
                GraphData = new[]
                {
                    new GraphData()
                    {
                        Data = data,
                        Fill = false,
                        BorderColor = "green",
                        BackgroundColor = "green",
                        Label = $"{_statisticsName} per level",
                    }
                }
            }
        };
    }
}