using AnalyticsTelegramBot.StateMachineNM.StatesNM.StatisticsGeneration.Data;
using Domain.Provider;
using Newtonsoft.Json;

namespace AnalyticsTelegramBot.StateMachineNM.StatesNM.StatisticsGeneration;

public class ChartRequest : IPNGProvider
{
    private readonly IProvider<ChartConfig> _configProvider;
    private readonly int _width;
    private readonly int _height;

    public ChartRequest(IProvider<ChartConfig> configProvider, int width = 500, int height = 300)
    {
        _configProvider = configProvider;
        _height = height;
        _width = width;
    }
    
    public async Task<byte[]> Provide()
    {
        ChartConfig config = await _configProvider.Provide();
        
        return await new ChartWrapper()
        {
            Width = _width,
            Height = _height,
            Config = JsonConvert.SerializeObject(config),
        }.ToByteArray();
    }
}