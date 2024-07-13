using AnalyticsTelegramBot.StateMachineNM.StatesNM.StatisticsGeneration.AverageNM;
using Application.Records;
using Domain.Provider;
using Domain.Repository.Commands.Contract;
using TelegramBotTemplate.StateMachineNM.State.AlbumState;

namespace AnalyticsTelegramBot.StateMachineNM.StatesNM.StatisticsGeneration;

public class QuickChartAlbumGeneration : IAlbumGeneration
{
    private readonly IRecordsDump<LevelRecord> _recordsDump;
    private readonly IProvider<string> _prefixNameProvider;

    public QuickChartAlbumGeneration(IProvider<string> prefixNameProvider, IRecordsDump<LevelRecord> recordsDump)
    {
        _prefixNameProvider = prefixNameProvider;
        _recordsDump = recordsDump;
    }

    public async Task<byte[][]> Generate()
    {
        Task<byte[]>[] imageTasks = await GetImageGenerationTasks();
        await Task.WhenAll(imageTasks);
        
        return imageTasks.Select(task => task.Result).ToArray();
    }

    private async Task<Task<byte[]>[]> GetImageGenerationTasks()
    {
        string prefix = await _prefixNameProvider.Provide();
        
        return new[]
        {
            GetImageGenerationTask(prefix + " time", "green", record => record.Time),
            GetImageGenerationTask(prefix + " death", "black", record => record.DeathCount),
            GetImageGenerationTask(prefix + " stars", "orange", record => record.Stars),
        };
    }

    private Task<byte[]> GetImageGenerationTask(string name, string color, Func<LevelRecord, double> observation)
    {
        AverageLevelsStatisticsQuery configProvider = new(_recordsDump, name, color, observation);
        IPNGProvider imageProvider = new ChartRequest(configProvider);
        
        return imageProvider.Provide();
    }
}