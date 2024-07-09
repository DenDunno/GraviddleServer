using GraviddleServer.Code.Bot.StateMachineNM.StatesNM.StatisticsGeneration.Data;
using Newtonsoft.Json;

namespace GraviddleServer.Code.Bot.StateMachineNM.StatesNM.StatisticsGeneration;

public class ChartRequest : IPNGProvider
{
    private readonly IConfigProvider _configProvider;
    private readonly int _width;
    private readonly int _height;

    public ChartRequest(IConfigProvider configProvider, int width = 500, int height = 300)
    {
        _configProvider = configProvider;
        _height = height;
        _width = width;
    }
    
    public async Task<byte[]> Evaluate()
    {
        ChartConfig config = _configProvider.GetConfig();
        
        return await new ChartWrapper()
        {
            Width = _width,
            Height = _height,
            Config = JsonConvert.SerializeObject(config),
        }.ToByteArray();
    }
}